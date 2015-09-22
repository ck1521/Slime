function EngineIndexViewModel(engines) {
    var self = this;
    self.engines = engines;

    if (typeof PagingService === 'function') {
        self.pagingService = new PagingService(engines);
    }

    self.showDeleteModal = function (data, event) {
        self.sending = ko.observable(false);
        $.get($(event.target).attr('href'), function (d) {
            $('.body-content').prepend(d);
            $('#deleteModal').modal('show');

            ko.applyBindings(self, document.getElementById('deleteModal'));
        });
    };

    // For delete
    self.engine = {
        id: ko.observable(),
        name: ko.observable(),
        mobility: ko.observable(),
        stage:ko.observable(),
    };


    self.deleteAuthor = function (form) {
        self.sending(true);
        return true;
    };


    self.sending = ko.observable(false);
    self.showDeleteModalAjax = function (data, event) {
        $.get(
            $(event.target).attr('href'),
            function (d) {
                self.engine.id(d.id);
                self.engine.name(d.name);
                self.engine.mobility(d.mobility);
                self.engine.stage(d.stage);
                $('#deleteModal').modal('show');
            });
    };


    self.deleteAuthorAjax = function (form) {
        self.sending(true);
        $.ajax({
            url: '/api/authors/' + self.engine.id(),
            method: 'delete',
            contentType: 'application/json',
        })
        .success(self.saveSuccess)
        .error(self.saveError)
        .complete(function () { self.sending(false); });
    };

    self.saveSuccess = function () {
        $('.body-content').prepend(
            '<div class="alert alert-success"><strong>Success!</strong>The author has been removed.</div>'
            );
        setTimeout(function () { location.href = '../'; }, 1000);
    };

    self.saveError = function () {
        $('.body-content').prepend(
             '<div class="alert alert-danger"><strong>Error!</strong>Some error happened.</div>'
            );
    };
}