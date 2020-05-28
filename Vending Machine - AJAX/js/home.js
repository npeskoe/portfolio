$(document).ready(function () {

    loadItems();

    var totalMoneyIn = 0;

    $('#add-dollar-button').click(function (event) {

        totalMoneyIn += 1.00;

        $('#change-display').val('$' + (totalMoneyIn).toFixed(2));

    });

    $('#add-quarter-button').click(function (event) {

        totalMoneyIn += 0.25;

        $('#change-display').val('$' + (totalMoneyIn).toFixed(2));

    });

    $('#add-dime-button').click(function (event) {

        totalMoneyIn += 0.1;

        $('#change-display').val('$' + (totalMoneyIn).toFixed(2));

    });

    $('#add-nickel-button').click(function (event) {

        totalMoneyIn += 0.05;

        $('#change-display').val('$' + (totalMoneyIn).toFixed(2));

    });

    $('#purchase-button').click(function (event) {

        $.ajax({
            type: 'GET',
            url: 'http://localhost:8080/money/' + totalMoneyIn.toFixed(2) + '/item/' + $('#itemDisplay').val(),

            success: function (data, status) {
                $.each(data, function (index, item) {

                    var message = data.message;
                    var quarters = data.quarters;
                    var dimes = data.dimes;
                    var nickels = data.nickels;
                    var pennies = data.pennies;

                    $('#depositDisplay').val("Thank You!!!");

                    var totalChange = 0;

                    if (quarters > 0) {
                        totalChange += (quarters * 0.25);
                    }

                    if (dimes > 0) {
                        totalChange += (dimes * 0.10);
                    }

                    if (nickels > 0) {
                        totalChange += (nickels * 0.05);
                    }

                    if (pennies > 0) {
                        totalChange += (pennies * 0.01);
                    }

                    $('#changeDisplay').val('$' + (totalChange).toFixed(2));

                });
            },           
            error: function (data, responseText, message){
                var error422 = JSON.parse(data.responseText);
                $('#depositDisplay').val((JSON.stringify(error422.message)));
            }
        });
    });

    $('#change-return').click(function (event) {

        totalMoneyIn = 0;
        $('#change-display').val("");
        $('#itemDisplay').val("");
        $('#depositDisplay').val("");
        $('#changeDisplay').val("");
        clearItemTable();
        loadItems();

    });
});

function loadItems() {

    $.ajax({
        type: 'GET',
        url: 'http://localhost:8080/items',
        success: function (data, status) {
            $.each(data, function (index, item) {
                var name = item.name;
                var price = item.price;
                var quantity = item.quantity;
                var id = item.id;

                var itemDiv = "<a style='text-decoration: none; color:inherit' onclick='itemSelect(" + id + ")'><div class='card mr-5 mb-5' style='width: 10rem'>";

                itemDiv += "<h5>" + id + "</h5>";
                itemDiv += "<h6 class='card-subtitle mb2 text-center'>" + name + "</h6>";
                itemDiv += "<p class='card-text text-center'>$" + price.toFixed(2) + "</p>";
                itemDiv += "<p class='card-text text-center'>Quantity Left:" + " " + quantity + "</p>";
                itemDiv += "</div></a>";

                $('#vendTableDiv').append(itemDiv);
            });
        },
        error: function () {
            alert("Vending Machine Items Failed to Load")
        }
    });
}

function clearItemTable() {
    $('#vendTableDiv').empty();
}

function itemSelect(id) {

    var itemSelected = id;

    $('#itemDisplay').val(itemSelected)

    return itemSelected;

}



