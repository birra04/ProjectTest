
function ReservationViewModel() {
    var self = this;
    self.reservationList = ko.observableArray([]);
    self.viewPagingControl = ko.computed(() => { return self.reservationList().length > 0 });
    self.ReservationById = function (reservationId) {
        if (reservationId > 0) {
            var reservationsId =
                { Id: reservationId };
            var json = JSON.stringify(reservationsId);
            $.ajax({
                url: "/reservation/getreservation",
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

    self.setRankingValue = function (ranking, id) {
        for (var i = 1; i <= 5; i++) {
            var cur = document.getElementById("star" + i + id);
            if (cur != null) {
                if (ranking >= i)
                    cur.className = "fa fa-star checked";
                else
                    cur.className = "fa fa-star";
            }
        }
    };

    self.setRanking = function (ranking, id) {
        var item = document.getElementById("star" + ranking + id);
        if (item != null) {
            self.setRankingValue(ranking, id);
            var reservations = {
                ReservationId: id,
                Ranking: ranking
            };
            $.ajax({
                url: '/reservation/updateranking',
                type: 'post',
                contentType: 'application/x-www-form-urlencoded',
                data: ko.toJS(reservations)
            });
        }
    };

    self.onclick = function (Reservation) {
        var reservations = {
            ReservationId: Reservation.ReservationId,
        };
        console.log("encontro");
        var url = "/reservation/addtofavorites";
        ajaxFunction(url, "POST", reservations).done(function () {
        });
    }

    function getReservations() {
        $.ajax({
            url: "/reservation/getreservations",
            type: "GET",
            contentType: "application/javascript",
            success: function (data) {
                //var observableData = ko.mapping.fromJS(data);
                //var array = observableData();
                self.reservationList(data);
            },
            error: function (error) {
                alert(error);
            }
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
        })
            .fail(function (errorThrown) {
                alert("Error  " + errorThrown);
            });
    }

    getReservations();
}
ko.applyBindings(new ReservationViewModel());

