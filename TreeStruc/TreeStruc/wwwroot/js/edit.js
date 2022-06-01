var pull = function (categoryID) {
    $.ajax({
        type: 'POST',
        url: "/Tree/EditPart",
        data: { "Id": categoryID },
        success: function (response) {
            $('#modalBody').html(response);
            $('#edit-modal').modal('show');
        }
    })
}
