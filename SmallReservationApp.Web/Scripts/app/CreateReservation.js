ko.validation.init({

    registerExtenders: true,
    messagesOnModified: true,
    insertMessages: true,
    parseInputAttributes: true,
    errorClass: 'errorStyle',
    messageTemplate: null

}, true);

var checkBirthDay = function (val) {
    var now = new Date();
    var birthday = new Date(val);

    return !(birthday > now);
};

function EditReservationViewModel(model, resourceTexts) {

    var self = this
    self.isNew = model.Contact === null;
    self.id = model.ReservationId;
    self.description = ko.observable(model.Description).extend({ required: true, });
    self.reservationDate = model.ReservationDate;
    self.ranking = model.Ranking;
    self.favorite = model.Favorite;
    self.saveCompleted = ko.observable(false);
    self.sending = ko.observable(false);
    self.resourceTexts = resourceTexts;

    if (model.Contact !== null) {
        self.contactid = model.Contact.ContactId;
        self.contact = model.Contact;
        self.name = ko.observable(model.Contact.Name).extend({
            required: true,
            minLength: 2,
            maxLength: 17,
            pattern: {
                message: self.resourceTexts.pleaseJustLetters,
                params: '^[a-zA-Z ]*$'
            }
        });
        self.birthDate = ko.observable(new Date(model.Contact.BirthDate).toISOString().split('T')[0]).extend({
            validation: {
                validator: checkBirthDay,
                message: self.resourceTexts.futureDate
            }
        });
        self.contactType = ko.observable(model.Contact.ContactType).extend({ required: true });
        self.phoneNumber = ko.observable(model.Contact.PhoneNumber).extend({
            required: true,
            pattern: {
                message: self.resourceTexts.phoneNumberNotMatch,
                params: '^[0-9]{2}[0-9]{2}[0-9]{2}[0-9]{2}$'
            }
        });
    }
    else {
        self.contactType = ko.observable("");
        self.name = ko.observable().extend({
            required: true,
            minLength: 2,
            maxLength: 17,
            pattern: {
                message: self.resourceTexts.pleaseJustLetters,
                params: '^[a-zA-Z ]*$'
            }
        });
        self.birthDate = ko.observable(new Date().toISOString().split('T')[0]).extend({
            validation: {
                validator: checkBirthDay,
                message: self.resourceTexts.futureDate
            }
        });
        self.contactType = ko.observable().extend({ required: true });;
        self.phoneNumber = ko.observable().extend({
            pattern: {
                message: self.resourceTexts.phoneNumberNotMatch,
                params: '^[0-9]{2}[0-9]{2}[0-9]{2}[0-9]{2}$'
            }
        });;
    }

    jQuery.validator.setDefaults({
        ignore: ":hidden, [contenteditable='true']:not([name])"
    });

    ckeditor();

    //Edit Reservation
    function editReservation() {

        if (!self.errors().length == 0) {
            self.errors.showAllMessages();
            return;
        }
        var con = {
            ContactId: self.isNew ? 0 : self.contact.ContactId,
            name: self.name(),
            phoneNumber: self.phoneNumber(),
            birthDate: self.birthDate(),
            contactType: self.contactType()
        };

        var reservations = {
            ReservationId: self.isNew ? 0 : self.id,
            Contact: con,
            description: self.description(),
            contactid: self.contactid,
            reservationDate: self.reservationDate,
            ranking: self.ranking,
            favorite: self.favorite,
        };
        console.log("encontro");
        var url = "/reservation/editreservation";
        ajaxFunction(url, "POST", reservations).done(function () {
            alert(self.resourceTexts.saveSuccess);
        }).fail(function (err) {
            alert(self.resourceTexts.saveError);
        });
    }

    //OnClick Event to save reservation
    self.onClick = function () {
        editReservation();
    }

    //Delete Reservation  
    self.deleteReservation = function () {

        var reservation = {
            ReservationId: self.id
        };
        var url = "/reservation/deletereservation";
        ajaxFunction(url, "POST", reservation).done(function () {
            alert(self.resourceTexts.deleteSuccess);
        }).fail(function (err) {
            alert(self.resourceTexts.saveError);
        });
    }

    function ajaxFunction(uri, method, data) {
        return $.ajax({
            type: method,
            url: uri,
            dataType: "json",
            contentType: "application/json",
            data: data ? JSON.stringify(data) : null,
            cache: false,
            success: function (result) {
                if (result !== null) {
                    //        self.id(result.contact.ContactId);
                    //        self.name(result.contact.Name);
                    //        self.phoneNumber(result.contact.PhoneNumber);
                    //        self.birthDate(result.contact.BirthDate);
                    //        self.contactType(result.contact.ContactType);
                    //        self.profileDescription(result.contact.ProfileDescription);
                    window.location.href = result.url;
                }
            }
        });
    }

}

function ckeditor() {
    return ko.bindingHandlers.ckeditor = {
        init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
            var options = ko.utils.extend({
            }, allBindings.get('ckeditorOptions') || {
            });
            var modelValue = valueAccessor();

            var editor = CKEDITOR.replace(element, options);

            editor.on('change', function () {
                modelValue(editor.getData());
            });

            //handle disposal (if KO removes by the template binding)
            ko.utils.domNodeDisposal.addDisposeCallback(element, function () {
                if (editor) {
                    editor.destroy();
                };
            });
        },
        update: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
            var editor = new CKEDITOR.dom.element(element).getEditor();
            editor.setData(ko.unwrap(valueAccessor()), { internal: true, callback: null, noSnapshot: false });
        }
    };
}
