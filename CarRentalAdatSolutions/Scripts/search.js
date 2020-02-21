//search

$("#search").keyup(function () {

    $("#result").html('');

    var searchField = $("#search").val();

    var expression = new RegExp(searchField, "i");
    $.getJSON('/Vehicles/AllVehicles', function (data) {
        $.each(data.Data, function (key, value) {
            if (value.Name.search(expression) != -1) {
                $("#result").append("<a href='/Vehicles/Details/"+value.id+"'class='list-group-item'>" +
                    "<div class='card'>" +
                    "<div class='row'><div class='col-md-2'><img class='img-responsive img-rounded' src='" + value.Image + "'/></div>" +
                    "<div class='card-body'>" +
                    "<p class='card-tile'>" + value.Name + " <small class='card-text'>" + value.RentalPrice + "/Day</small></p>" +
                    "</div></div>" +
                    "</div>" +
                    "</a>");

            }
        });
    });
});