var TradingAnalyzer;
if (!TradingAnalyzer) TradingAnalyzer = {
    Log: {},
    Util: {},
    TradingAccount: {}
};

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

TradingAnalyzer.Log.showAddLogEntryModal = function (target) {
    $.ajax({
        type: "GET",
        url: abp.appPath + 'MarketLog/AddLogEntryModal',
        success: function (r) {
            $("#addLogEntryModalWrapper").html(r);
            TradingAnalyzer.Util.initForm("addLogEntryForm", TradingAnalyzer.Log.addLogEntry);
            TradingAnalyzer.Util.showModalForm("addLogEntryModal", false);

            document.getElementById("pasteTarget").
                addEventListener("paste", handlePaste);
        },
        contentType: "application/json"
    });
}

TradingAnalyzer.Log.addLogEntry = function (input) {
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

function handlePaste(e) {
    for (var i = 0; i < e.clipboardData.items.length; i++) {
        var item = e.clipboardData.items[i];
        if (item.type.indexOf("image") > -1) {
            var f = item.getAsFile();
            var reader = new FileReader();

            reader.onloadend = function () {
                $("#pasteTarget").hide();
                $("#imgScreenshot").attr("src", this.result).show();
                $("#Screenshot").val(this.result);
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
    $("#activeTradingAccount").html(name);
    abp.services.app.tradingAccount.setActive(id).done(function () {
        var tradingAccountsGrid = $("#tradingAccountsGrid");
        if (tradingAccountsGrid.size() > 0) {
            tradingAccountsGrid.data("kendoGrid").dataSource.read();
        }
    });
}
