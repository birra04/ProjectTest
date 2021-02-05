function ContactsViewModel() {
    var self = this;
    self.contactList = ko.observableArray([]);

    self.ContactById = function (contactId) {
        if (contactId > 0) {
            var contactsId =
                { Id: contactId };
            var json = JSON.stringify(contactsId);
            $.ajax({
                url: "/contact/getcontact",
                type: "GET",
                contentType: "application/javascript",
                data: json ? JSON.stringify(json) : null,
                success: function (result) {
                    console.log(result);
                },
                error: function (error) {
                    alert(error);
                }
            });
        }
    }

    function getContacts() {
        $.ajax({
            url: "/contact/getcontacts",
            type: "GET",
            contentType: "application/javascript",
            success: function (data) {
                self.contactList(data);
            },
            error: function (error) {
                alert(error);
            }
        });
    }

    getContacts();
}
ko.applyBindings(new ContactsViewModel());
