// show the menu for all elements in the tree
document.querySelectorAll(".menu").forEach(menu => {
    menu.addEventListener("click", function (event) {
        let self = this;
        this.closest("li").querySelectorAll("li > .options").forEach(option => option.classList.toggle("hidden"));
        document.querySelectorAll(".menu").forEach(menu => {
            if (self !== menu) {
                menu.querySelectorAll(".menu > options").forEach(option => option.classList.add("hidden"));
            }
        });
        this.closest("li").querySelectorAll("li > span > i").forEach(i => {
            i.classList.toggle("fa-info-circle");
            i.classList.toggle("fa-times-circle");
        });
    });
});


// collapse the entire tree
document.querySelector('.collapse-all').addEventListener('click', () => {
    $('.collapsive').collapse('toggle');
});

$('.tree-collapse').click(function () {
    $(this).parent('li').children('ul').collapse('toggle');
    $(this).parent().children('#menu').children('i').toggleClass('fa-plus');
    $(this).parent().children('#menu').children('i').toggleClass('fa-minus');
});


// remove the nodes
var url;
var redirectUrl;
var target;

$('.delete').on('click', (e) => {
    e.preventDefault();
    target = e.target;
    var bodyMessage = $(target).data('body-message');
    $('.modal-body-message').text(bodyMessage);
    $('#delete-modal').modal('show');
});


$('#confirm-delete').on('click', (e) => {
    var Id = $(target).data('id');
    Delete(Id, e);
});

function Delete(id, event) {
    var categoryObj = {
        Name: $('#name').val(),
        ParentID: id,
    };

    $.ajax({
        url: "/Tree/Delete/" + id,
        data: JSON.stringify(categoryObj),
        type: "DELETE",
        contentType: "application/json:charset=UTF-8",
        dataType: "json",
        success: function (data) {
            $('#delete-modal').modal('hide');
            setInterval(() => location.reload(true), 300);
        },
        error: function (data) {
        }
    });
    return this.data;
}







