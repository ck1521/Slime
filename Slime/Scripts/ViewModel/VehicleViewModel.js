function VehicleViewModel(vehicle)
{
    var self = this;
    self.sending = ko.observable(false);
    self.saveCompleted = ko.observable(false);

    self.vehicle =
    {
        id: vehicle.id,
        name: ko.observable(vehicle.name),
        fp: ko.observable(vehicle.fp),
        dr: ko.observable(vehicle.dr)
    };

    self.submit = function (form) {
        if (!$(form).valid()) {
            return false;
        }
        self.sending(true);
        self.vehicle.__RequestVerificationToken = form[0].value;
        $.ajax({
            url: 'Create',
            method: 'POST',
            contentType: 'application/x-www-form-urlencoded',
            data: ko.toJS(self.vehicle)
        })
        .success(self.saveSuccess)
        .error(self.saveError)
        .complete(function () { self.sending(false); });
    };
    
    self.saveSuccess = function () {
        self.saveCompleted(true);

        $('.body-content').prepend(
            '<div class="alert alert-success"><strong>Success!</strong>The item has been saved.</div>'
            );

        if (self.isCreating) {
            setTimeout(function () {
                location.href = '../';
            }, 1000);

        }
        else {
            setTimeout(function () { location.href = '../'; }, 1000);
        }
    };

    self.saveError = function () {
        $('.body-content').prepend(
             '<div class="alert alert-danger"><strong>Error!</strong>Some error happened.</div>'
            );
    };
}