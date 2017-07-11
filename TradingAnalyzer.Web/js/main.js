var TradingAnalyzer;
if (!TradingAnalyzer) TradingAnalyzer = {
    Log: {},
    Util: {}
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
    _$modal.modal("hide");
}

TradingAnalyzer.Log.showAddLogEntryModal = function (target) {
    $.ajax({
        type: "GET",
        url: abp.appPath + 'ViewRenderer/AddLogEntryModal',
        success: function (r) {
            $("#addLogEntryModalWrapper").html(r);
            TradingAnalyzer.Util.initForm("addLogEntryForm", TradingAnalyzer.Log.addLogEntry);
            TradingAnalyzer.Util.showModalForm("addLogEntryModal", false);
        },
        contentType: "application/json"
    });
}

TradingAnalyzer.Log.addLogEntry = function (input) {
    TradingAnalyzer.Util.hideModalForm("addLogEntryModal");
    abp.services.app.marketLogEntry.add(input).done(function () { });
}
