ko.validation.init({

    registerExtenders: true,
    messagesOnModified: true,
    insertMessages: true,
    parseInputAttributes: true,
    errorClass: 'errorStyle',
    messageTemplate: null

}, true);

var checkBirthday = function (val) {
    var now = new Date();
    var birthday = new Date(val);

    return !(birthday > now);
};

function EditContactViewModel(model, resourceTexts) {

    var self = this;
    self.id = model.ContactId;
    self.resourceTexts = resourceTexts;
    self.name = ko.observable(model.Name).extend({
        required: true,
        minLength: 2,
        maxLength: 17,
        pattern: {
            message: self.resourceTexts.pleaseJustLetters,
            params: '^[a-zA-Z ]*$'
        }
    });
    self.birthDate = ko.observable(new Date(model.BirthDate).toISOString().split('T')[0]).extend({
        validation: {
            validator: checkBirthday,
            message: self.resourceTexts.futureDate
        }
    });
    self.contactType = ko.observable(model.ContactType).extend({ required: true });
    self.phoneNumber = ko.observable(model.PhoneNumber).extend({
        required: true,
        pattern: {
            message: self.resourceTexts.phoneNumberNotMatch,
            params: '^[0-9]{2}[0-9]{2}[0-9]{2}[0-9]{2}$'
        }
    });

    function editContact() {

        if (!self.errors().length == 0) {
            self.errors.showAllMessages();
            return;
        }

        var contacts = {
            ContactId: self.id,
            name: self.name(),
            phoneNumber: self.phoneNumber(),
            birthDate: self.birthDate(),
            contactType: self.contactType(),
        };
        console.log("encontro");
        var url = "/contact/editcontact";
        ajaxFunction(url, "POST", contacts).done(function () {
            alert(self.resourceTexts.saveSuccess);
        }).fail(function (err) {
            alert(self.resourceTexts.saveError);
        });
    }

    self.onClick = function () {

        editContact();
    }

    //Delete Contact  
    self.deleteContact = function () {

        var contacts = {
            ContactId: self.id
        };
        var url = "/contact/deletecontact";
        ajaxFunction(url, "POST", contacts).done(function () {
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
            success: function (result) {
                if (result !== null) {
                    window.location.href = result.url;
                }
            }
        }).fail(function (errorThrown) {
            alert(self.resourceTexts.saveError);
        });
    }
}
