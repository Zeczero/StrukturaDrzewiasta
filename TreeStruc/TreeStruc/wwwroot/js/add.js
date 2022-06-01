

/**
 * Showing pop-up window to add node
 * */
$(".add").on('click', (e) => {
    e.preventDefault();
    target = e.target;
    var bodyMessage = $(target).data('body-message');

    $('.modal-body-message').text(bodyMessage);
    $('#add-modal').modal('show');
});

$('#confirm-add').on('click', (e) => {
    var Id = $(target).data('id');
    if (!($.isNumeric($('#name').val()))) {
        $('add-input-name').val("The data can only be of a numeric value.");
    } else {
        Add(Id, e);
    }

    function Add(id, event) {
        var ev = event;
        var categoryObj = {
            Name: $('#name').val(),
            ParentID: id,
        };
        if (categoryObj.Name.length < 1) {

            $('#add-error').text("You have to enter at least 2 characters!");
        } else {
            $.ajax({
                url: "/Tree/Create",
                data: JSON.stringify(categoryObj),
                type: "POST",
                contentType: "application/json:charset=UTF-8",
                dataType: "json",
                success: function (data) {
                    $('#add-modal').modal('hide');
                    setInterval(() => location.reload(true), 300);
                },
                error: function (data) {
                }
            });
        }

        return this.data;
    }
});

