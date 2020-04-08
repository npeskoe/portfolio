$(document).ready(function () {

    loadDVDs();

});

$('#create-dvd').click(function (event) {
    hideSearchBar();
    hideDVDs();
    showCreateForm();
})

$('#submit-dvd').click(function (event) {

    var haveValidationErrors = checkAndDisplayValidationErrors($('#create-dvd-form').find('input'));

    if (haveValidationErrors) {
        return false;
    }

    $.ajax({
        type: 'POST',
        url: 'http://localhost:49198/dvd',
        data: JSON.stringify({
            Title: $('#create-dvd-title').val(),
            ReleaseYear: $('#create-release-year').val(),
            Director: $('#create-director').val(),
            Rating: $('#createRatingSelect').val(),
            Notes: $('#create-notes').val()
        }),
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        'dataType': 'json',
        success: function (data, status) {
            $('#errorMessages').empty();

            $('#create-dvd-title').val('');
            $('#create-release-year').val('');
            $('#create-director').val('');
            $('#createRatingSelect').val('');
            $('#create-notes').val('');
            loadDVDs();
        },
        error: function () {
            $('#errorMessages')
                .append($('<li>')
                    .attr({ class: 'list-group-item list-group-item-danger' })
                    .text('Error calling web service. Please try again later.'));
        }
    });
})

$('#search-dvd-button').click(function (event) {

    var haveValidationErrors = checkAndDisplayValidationErrors($('#dvd-search-form').find('input'));

    if (haveValidationErrors) {
        return false;
    }

    var category = $('#dvdCategory').val();

    var searchTerm = $('#searchTerm').val();

    if (category == "title") {

        var contentRows = $('#contentRows');

        $.ajax({
            type: 'GET',
            url: 'http://localhost:49198/dvds/title/' + searchTerm,
            success: function (data, status) {
                $.each(data, function (index, dvd) {
                    var title = dvd.Title;
                    var releaseDate = dvd.ReleaseYear;
                    var director = dvd.Director;
                    var rating = dvd.Rating;
                    var id = dvd.DvdID;

                    var row = '<tr>';
                    row += '<td><a onclick="showDVDDetails(' + id + ')">' + title + '</td>';
                    row += '<td>' + releaseDate + '</td>';
                    row += '<td>' + director + '</td>';
                    row += '<td>' + rating + '</td>';
                    row += '<td><a onclick="showEditForm(' + id + ')">Edit</a></td>';
                    row += '<td><a onclick="confirmDelete(' + id + ')">Delete</a></td>';
                    row += '</tr>'

                    contentRows.empty().append(row);
                });
            },

        })
    }
    if (category == "release") {

        var contentRows = $('#contentRows');

        $.ajax({
            type: 'GET',
            url: 'http://localhost:49198/dvds/year/' + searchTerm,
            success: function (data, status) {
                $.each(data, function (index, dvd) {
                    var title = dvd.Title;
                    var releaseDate = dvd.ReleaseYear;
                    var director = dvd.Director;
                    var rating = dvd.Rating;
                    var id = dvd.DvdID;

                    var row = '<tr>';
                    row += '<td><a onclick="showDVDDetails(' + id + ')">' + title + '</td>';
                    row += '<td>' + releaseDate + '</td>';
                    row += '<td>' + director + '</td>';
                    row += '<td>' + rating + '</td>';
                    row += '<td><a onclick="showEditForm(' + id + ')">Edit</a></td>';
                    row += '<td><a onclick="confirmDelete(' + id + ')">Delete</a></td>';
                    row += '</tr>'

                    contentRows.empty().append(row);
                });
            },
        })
    }
    if (category == "director") {

        var contentRows = $('#contentRows');

        $.ajax({
            type: 'GET',
            url: 'http://localhost:49198/dvds/director/' + searchTerm,
            success: function (data, status) {
                $.each(data, function (index, dvd) {
                    var title = dvd.Title;
                    var releaseDate = dvd.ReleaseYear;
                    var director = dvd.Director;
                    var rating = dvd.Rating;
                    var id = dvd.DvdID;

                    var row = '<tr>';
                    row += '<td><a onclick="showDVDDetails(' + id + ')">' + title + '</td>';
                    row += '<td>' + releaseDate + '</td>';
                    row += '<td>' + director + '</td>';
                    row += '<td>' + rating + '</td>';
                    row += '<td><a onclick="showEditForm(' + id + ')">Edit</a></td>';
                    row += '<td><a onclick="confirmDelete(' + id + ')">Delete</a></td>';
                    row += '</tr>'

                    contentRows.empty().append(row);
                });
            },
        })
    }
    if (category == "rating") {

        var contentRows = $('#contentRows');

        $.ajax({
            type: 'GET',
            url: 'http://localhost:49198/dvds/rating/' + searchTerm,
            success: function (data, status) {
                $.each(data, function (index, dvd) {
                    var title = dvd.Title;
                    var releaseDate = dvd.ReleaseYear;
                    var director = dvd.Director;
                    var rating = dvd.Rating;
                    var id = dvd.DvdID;

                    var row = '<tr>';
                    row += '<td><a onclick="showDVDDetails(' + id + ')">' + title + '</td>';
                    row += '<td>' + releaseDate + '</td>';
                    row += '<td>' + director + '</td>';
                    row += '<td>' + rating + '</td>';
                    row += '<td><a onclick="showEditForm(' + id + ')">Edit</a></td>';
                    row += '<td><a onclick="confirmDelete(' + id + ')">Delete</a></td>';
                    row += '</tr>'

                    contentRows.empty().append(row);
                });
            },
            error: function () {
                $('#errorMessages')
                    .append($('<li>')
                        .attr({ class: 'list-group-item list-group-item-danger' })
                        .text('Error calling web service. Please try again later.'));
            }
        });
    }
})



$('#cancel-create-button').click(function (event) {
    showSearchBar();
    hideCreateForm();
})


function loadDVDs() {
    clearDVDTable();
    hideCreateForm();
    hideEditForm();
    hideDVDDetails();

    var contentRows = $('#contentRows');

    $.ajax({
        type: 'GET',
        url: 'http://localhost:49198/dvds',
        success: function (data, status) {
            $.each(data, function (index, dvd) {
                var title = dvd.Title;
                var releaseDate = dvd.ReleaseYear;
                var director = dvd.Director;
                var rating = dvd.Rating;
                var id = dvd.DvdID;

                var row = '<tr>';
                row += '<td><a onclick="showDVDDetails(' + id + ')">' + title + '</td>';
                row += '<td>' + releaseDate + '</td>';
                row += '<td>' + director + '</td>';
                row += '<td>' + rating + '</td>';
                row += '<td><a onclick="showEditForm(' + id + ')">Edit</a></td>';
                row += '<td><a onclick="confirmDelete(' + id + ')">Delete</a></td>';
                row += '</tr>'
                contentRows.append(row);
            });
        },
        error: function () {
            $('#errorMessages')
                .append($('<li>')
                    .attr({ class: 'list-group-item list-group-item-danger' })
                    .text('Error calling web service. Please try again later.'));
        }
    });
}

function checkAndDisplayValidationErrors(input) {
    $('#errorMessages').empty();

    var errorMessages = [];

    input.each(function () {

        if (!this.validity.valid) {
            var errorField = $('label[for=' + this.id + ']').text();
            errorMessages.push(errorField + ' ' + this.validationMessage);
        }
    });

    if (errorMessages.length > 0) {
        $.each(errorMessages, function (index, message) {
            $('#errorMessages').append($('<li>').attr({ class: 'list-group-item list-group-item-danger' }).text(message));
        });
        return true;
    } else {
        return false;
    }
}

function clearDVDTable() {
    $('#contentRows').empty();
}

function hideEditForm() {
    $('#errorMessages').empty();
    $('#edit-dvd-title').val('');
    $('#edit-release-year').val('');
    $('#edit-director').val('');
    $('#edit-rating').val('');
    $('#edit-notes').val('');
    $('#edit-dvd-table').hide();
    $('#dvdTableDiv').show();
}

function hideCreateForm() {
    $('#errorMessages').empty();
    $('#create-dvd-title').val('');
    $('#create-release-year').val('');
    $('#create-director').val('');
    $('#createRatingSelect').val('');
    $('#create-notes').val('');
    $('#create-dvd-table').hide();
    $('#dvdTableDiv').show();
}

function showCreateForm() {
    $('#errorMessages').empty();
    $('#create-dvd-table').show();
}

function hideDVDs() {
    $('#dvdTableDiv').hide();
}

function hideDVDDetails() {
    $('#dVDDet').hide();
}

function showEditForm(dvdId) {

    $('#errorMessages').empty();

    $.ajax({
        type: 'GET',
        url: 'http://localhost:49198/dvd/' + dvdId,
        success: function (data, status) {
            $('#edit-dvd-title').val(data.Title);
            $('#edit-release-year').val(data.ReleaseYear);
            $('#edit-director').val(data.Director);
            $('#ratingGroupSelect').val(data.Rating);
            $('#edit-notes').val(data.Notes);
            $('#edit-dvd-id').val(data.DvdID);

            editDVDTitle.append(' ' + data.Title);
        },
        error: function () {
            $('#errorMessages')
                .append($('<li>')
                    .attr({ class: 'list-group-item list-group-item-danger' })
                    .text('Error calling web service. Please try again later.'));
        }
    });
    hideSearchBar();
    $('#dvdTableDiv').hide();
    $('#edit-dvd-table').show();
}

function showDVDDetails(dvdId) {

    $('#errorMessages').empty();

    $.ajax({
        type: 'GET',
        url: 'http://localhost:49198/dvd/' + dvdId,
        success: function (data, status) {

            var title = data.Title;
            var releaseDate = data.ReleaseYear;
            var director = data.Director;
            var rating = data.Rating;
            var notes = data.Notes;
            var id = data.DvdID;

            dvdTitle.append(title);
            relYear.append(releaseDate);
            dir.append(director);
            rate.append(rating);
            note.append(notes);

        },
        error: function () {
            $('#errorMessages')
                .append($('<li>')
                    .attr({ class: 'list-group-item list-group-item-danger' })
                    .text('Error calling web service. Please try again later.'));
        }
    });
    hideSearchBar();
    hideDVDs();
    $('#dVDDet').show();
}

function deleteDVD(dvdId) {

    $.ajax({
        type: 'DELETE',
        url: 'http://localhost:49198/dvd/' + dvdId,
        success: function (status) {
            loadDVDs();
        }
    });
}

$('#cancel-edit-button').click(function (event) {

    showSearchBar();
    hideEditForm();
})

function hideSearchBar() {
    $('#searchBar').hide();
}

function showSearchBar() {
    $('#searchBar').show();
}

function confirmDelete(dvdId) {
    var c = confirm("Are you sure you want to delete this Dvd from your collection?")

    if (c == true) {
        deleteDVD(dvdId);
    }
    else (c == false);
}

function showDVD() {
    $('#title').click(function (event) {
        confirmDelete();
    })
}

$('#edit-changes-button').click(function (event) {

    var haveValidationErrors = checkAndDisplayValidationErrors($('#edit-dvd-form').find('input'));

    if (haveValidationErrors) {
        return false;
    }

    $.ajax({
        type: 'PUT',
        url: 'http://localhost:49198/dvd/' + $('#edit-dvd-id').val(),
        data: JSON.stringify({
            DvdID: $('#edit-dvd-id').val(),
            Title: $('#edit-dvd-title').val(),
            ReleaseYear: $('#edit-release-year').val(),
            Director: $('#edit-director').val(),
            Rating: $('#ratingGroupSelect').val(),
            Notes: $('#edit-notes').val()
        }),
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        'dataType': 'json',
        success: function () {

            $('#errorMessages').empty();
            hideEditForm();
            loadDVDs();
        },
        error: function () {
            $('#errorMessages')
                .append($('<li>')
                    .attr({ class: 'list-group-item list-group-item-danger' })
                    .text('Error calling web service. Please try again later.'));
        }
    })
})