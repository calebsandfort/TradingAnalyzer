var TradingAnalyzer;
if (!TradingAnalyzer) TradingAnalyzer = {
    Log: {},
    Console: {},
    Util: {},
    TradingAccount: {},
    Trade: {},
    Market: {
        Markets: [
            { Name: "E-Mini NASDAQ 100", Symbol: "ES", TickSize: .25, TickValue: 5, InitialMargin: 4620 },
            { Name: "E-Mini S&P 500", Symbol: "NQ", TickSize: .25, TickValue: 12.50, InitialMargin: 4290 },
            { Name: "E-Mini Dow", Symbol: "YM", TickSize: 1, TickValue: 5, InitialMargin: 3905 },
            { Name: "Gold", Symbol: "GC", TickSize: .10, TickValue: 10, InitialMargin: 4345 },
            { Name: "Oil", Symbol: "CL", TickSize: .01, TickValue: 10, InitialMargin: 2750 }
        ]
    },
    TradeExitReasons: [
        { Value: 0, Display: "None", ExitPriceField: "EntryPrice" },
        { Value: 1, Display: "Target Hit", ExitPriceField: "ProfitTakerPrice" },
        { Value: 2, Display: "Stop Loss Hit", ExitPriceField: "StopLossPrice" },
        { Value: 3, Display: "Reversal Signal", ExitPriceField: "EntryPrice" },
        { Value: 4, Display: "End of Day", ExitPriceField: "EntryPrice" }
    ]
};

//[EnumDisplay("None")]
//None,
//    [EnumDisplay("Target Hit")]
//TargetHit,
//    [EnumDisplay("Stop Loss Hit")]
//StopLossHit,
//    [EnumDisplay("Reversal Signal")]
//ReversalSignal,
//    [EnumDisplay("End of Day")]
//EndOfDay

(function ($) {

    //Notification handler
    abp.event.on('abp.notifications.received', function (userNotification) {
        abp.notifications.showUiNotifyForUserNotification(userNotification);
    });

    //serializeFormToObject plugin for jQuery
    $.fn.serializeFormToObject = function () {
        //serialize to array
        var data = $(this).serializeArray();

        //add also disabled items
        $(':disabled[name]', this).each(function () {
            data.push({ name: this.name, value: $(this).val() });
        });

        //map to object
        var obj = {};
        data.map(function (x) { obj[x.name] = x.value; });

        return obj;
    };

    //Configure blockUI
    if ($.blockUI) {
        $.blockUI.defaults.baseZ = 2000;
    }
})(jQuery);

$(function () {
    $("body").on("click", ".expandScreenshot", TradingAnalyzer.Util.expandScreenshotClick);
});

$(document).ready(function () {
    var consoleHub = $.connection.consoleHub; //get a reference to the hub

    consoleHub.client.writeLine = function (line) { //register for incoming messages
        TradingAnalyzer.Console.writeLine(line);
    };
});

TradingAnalyzer.Console.clear = function () {
    $("#consoleWell").html("");
}

TradingAnalyzer.Console.writeLine = function (line) {
    $("#consoleWell").prepend("<div>" + line + "</div>");
}

TradingAnalyzer.Util.expandScreenshotClick = function () {
    var expandScreenshotModal = $("#expandScreenshotModal");
    expandScreenshotModal.find("img").attr("src", $(this).attr("src"));
    expandScreenshotModal.modal("show");
}

TradingAnalyzer.Util.initForm = function (id, submitFunc) {
    var _$form = $('#' + id);

    _$form.validate({
        ignore: ":hidden:Not(.includeHidden), .ignoreValidation"
    });

    _$form.find('button[type=submit]')
        .click(function (e) {
            e.preventDefault();

            if (!_$form.valid()) {
                return;
            }

            var input = _$form.serializeFormToObject();

            _$form.find("select[multiple=multiple]").each(function () {
                var listBox = $(this);
                var id = listBox.attr("id");

                if (listBox.val() == null) {
                    input[id] = [];
                }
                else {
                    input[id] = listBox.val();
                }
            });

            abp.ui.setBusy('#' + id);

            submitFunc(input);
        });
}

TradingAnalyzer.Util.showModalForm = function (id, clearForm) {
    var _$modal = $('#' + id);
    var _$form = _$modal.find("form");

    _$modal.modal("show");

    if (_$form.size() > 0) {
        setTimeout(function () {
            if (typeof (clearForm) == "undefined" || clearForm) {
                _$form.find('input').val("");
            }

            _$form.find('input:first').focus();
        }, 500);
    }
}

TradingAnalyzer.Util.hideModalForm = function (id) {
    var _$modal = $('#' + id);
    abp.ui.clearBusy('#' + id);
    _$modal.modal("hide");
}

TradingAnalyzer.Util.hideEditField = function (container, name) {
    var label = container.find("label[for=" + name + "]");
    if (label.size() > 0) label.closest(".editor-label").hide();

    var field = container.find("[name=" + name + "]");
    if (field.size() > 0) field.closest(".editor-field").hide();
}

TradingAnalyzer.Log.showAddLogEntryModal = function () {
    $.ajax({
        type: "GET",
        url: abp.appPath + 'MarketLog/AddLogEntryModal',
        success: function (r) {
            $("#addLogEntryModalWrapper").html(r);
            TradingAnalyzer.Util.initForm("addLogEntryForm", TradingAnalyzer.Log.addLogEntry);
            TradingAnalyzer.Util.showModalForm("addLogEntryModal", false);

            document.getElementById("pasteTargetScreenshot").
                addEventListener("paste", function (e) {
                    TradingAnalyzer.Util.handlePaste("Screenshot", e);
                });
        },
        contentType: "application/json"
    });
}

TradingAnalyzer.Log.addLogEntry = function (input) {
    input.TradingAccountId = $("#activeTradingAccount").data("id");
    if (input.Screenshot == "{ id = Screenshot, class = includeHidden }") input.Screenshot = '';

    abp.services.app.marketLogEntry.add(input).done(function () {
        TradingAnalyzer.Util.hideModalForm("addLogEntryModal");
        TradingAnalyzer.Log.refresh();
    });
}

TradingAnalyzer.Log.purge = function () {
    abp.services.app.marketLogEntry.purge().done(function () {
        TradingAnalyzer.Log.refresh();
    });
}

TradingAnalyzer.Log.refresh = function (input) {
    $("#logListView").data("kendoListView").dataSource.read();
}

TradingAnalyzer.Util.handlePaste = function (fieldName, e) {
    for (var i = 0; i < e.clipboardData.items.length; i++) {
        var item = e.clipboardData.items[i];
        if (item.type.indexOf("image") > -1) {
            var f = item.getAsFile();
            var reader = new FileReader();

            reader.onloadend = function () {
                $("#pasteTarget" + fieldName).hide();
                $("#img" + fieldName).attr("src", this.result).show();
                $("#" + fieldName).val(this.result);
            };

            reader.readAsDataURL(f);
        } else {
            console.log("Discarding image paste data");
        }
    }
}

TradingAnalyzer.TradingAccount.grid_edit = function (e) {
    TradingAnalyzer.Util.hideEditField(e.container, "IsNew");
    TradingAnalyzer.Util.hideEditField(e.container, "Id");
}

TradingAnalyzer.TradingAccount.setActive = function (id, name) {
    $("#activeTradingAccount").html(name).data("id", id);
    abp.services.app.tradingAccount.setActive(id).done(function () {
        var tradingAccountsGrid = $("#tradingAccountsGrid");
        if (tradingAccountsGrid.size() > 0) {
            tradingAccountsGrid.data("kendoGrid").dataSource.read();
        }

        var logListView = $("#logListView");
        if (logListView.size() > 0) {
            logListView.data("kendoListView").dataSource.read();
        }
    });
}

TradingAnalyzer.Trade.grid_edit = function (e) {
    TradingAnalyzer.Util.hideEditField(e.container, "IsNew");
    TradingAnalyzer.Util.hideEditField(e.container, "Id");
    TradingAnalyzer.Util.hideEditField(e.container, "RefNumber");
    TradingAnalyzer.Util.hideEditField(e.container, "Market");
    TradingAnalyzer.Util.hideEditField(e.container, "ProfitLoss");
    TradingAnalyzer.Util.hideEditField(e.container, "TradingAccount");
    TradingAnalyzer.Util.hideEditField(e.container, "ProfitLossPerContract");
}

TradingAnalyzer.Trade.showTradeModal = function (id) {
    $.ajax({
        type: "GET",
        url: abp.appPath + 'Trades/TradeModal?id=' + id,
        success: function (r) {
            $("#tradeModalWrapper").html(r);
            TradingAnalyzer.Util.initForm("tradeForm", TradingAnalyzer.Trade.saveTrade);
            TradingAnalyzer.Util.showModalForm("tradeModal", false);

            if ($("#pasteTargetEntryScreenshot").size() > 0) {
                document.getElementById("pasteTargetEntryScreenshot").
                    addEventListener("paste", function (e) {
                        TradingAnalyzer.Util.handlePaste("EntryScreenshot", e);
                    });
            }

            if ($("#pasteTargetExitScreenshot").size() > 0) {
                document.getElementById("pasteTargetExitScreenshot").
                    addEventListener("paste", function (e) {
                        TradingAnalyzer.Util.handlePaste("ExitScreenshot", e);
                    });
            }
        },
        contentType: "application/json"
    });
}

TradingAnalyzer.Trade.exitReasonSelect = function (e) {
    var exitReasonInt = parseInt(e.dataItem.Value);
    var exitReason = _.find(TradingAnalyzer.TradeExitReasons, { 'Value': exitReasonInt });

    $("#ExitPrice").data("kendoNumericTextBox").value($("#" + exitReason.ExitPriceField).data("kendoNumericTextBox").value());
}

TradingAnalyzer.Trade.entryDateChange = function () {
    $("#ExitDate").data("kendoDateTimePicker").value(this.value());
}

TradingAnalyzer.Trade.entryPriceChange = function () {
    var value = this.value();
    $("#StopLossPrice").data("kendoNumericTextBox").value(value);
    $("#ProfitTakerPrice").data("kendoNumericTextBox").value(value);
}

TradingAnalyzer.Trade.marketChange = function () {
    var id = this.value();

    abp.services.app.market.get(id).done(function (market) {
        $("#Timeframe").data("kendoNumericTextBox").value(market.mtt);

        var currencyControls = ["EntryPrice", "StopLossPrice", "ProfitTakerPrice", "ExitPrice"];

        _.forEach(currencyControls, function (id) {
            $("#" + id).data("kendoNumericTextBox").step(market.tickSize);
        });
    });
}

TradingAnalyzer.Trade.purge = function () {
    abp.ui.setBusy('#tradesGrid');
    abp.services.app.trade.purge().done(function () {
        TradingAnalyzer.Trade.refresh();
        abp.ui.clearBusy('#tradesGrid');
    });
}

TradingAnalyzer.Trade.saveTrade = function (input) {
    if (input.EntryScreenshot == "{ id = EntryScreenshot, class = includeHidden }") input.EntryScreenshot = '';
    if (input.ExitScreenshot == "{ id = ExitScreenshot, class = includeHidden }") input.ExitScreenshot = '';

    abp.services.app.trade.save(input).done(function (reconcileTradingAccount) {
        TradingAnalyzer.Util.hideModalForm("tradeModal");
        TradingAnalyzer.Trade.refresh();
        TradingAnalyzer.Log.refresh();

        if (reconcileTradingAccount) {
            abp.services.app.tradingAccount.reconcile().done(function () {
                TradingAnalyzer.TradingAccount.refreshDetails();
            });
        }
    });
    
    TradingAnalyzer.Util.hideModalForm("tradeModal");
}

TradingAnalyzer.TradingAccount.purge = function () {
    abp.ui.setBusy('#tradesGrid');
    abp.services.app.tradingAccount.purge().done(function () {
        TradingAnalyzer.TradingAccount.refreshDetails();
        TradingAnalyzer.Trade.refresh();
        abp.ui.clearBusy('#tradesGrid');
    });
}

TradingAnalyzer.TradingAccount.refreshDetails = function (input) {
    $("#tradingAccountDetailsListView").data("kendoListView").dataSource.read();
}

TradingAnalyzer.Trade.refresh = function (input) {
    $("#tradesGrid").data("kendoGrid").dataSource.read();
}
