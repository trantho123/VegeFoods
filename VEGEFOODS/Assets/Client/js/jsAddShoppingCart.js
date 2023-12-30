
$(document).ready(function () {
    ShowCount();
    $('body').on('click', '.btnAddToCart', function (e) {
        e.preventDefault();
        var id = $(this).data('id');
        var quantity = 1;

        //var quantity = parseInt($('#quantity').val());
        var tQuantity = parseInt($('#quantity').val());
        if (!isNaN(tQuantity)) {
            quantity = parseInt(tQuantity);
        }

        $.ajax({
            url: '/ClientCart/AddToCart?id=' + id + '&quantity=' + quantity,
            type: 'Post',
            success: function (rs) {
                if (rs.Success) {
                    $('#checkout-item').html(rs.Count);
                  //  alert(rs.msg);

                }
                else {
                    alert(rs.msg);
                }
            }
            
        });
    });
    $('body').on('click', '.btnDelete', function (e) {
        e.preventDefault();
        var id = $(this).data('id');
        var conf = confirm('Bạn có chắc muốn xóa sản phẩm này khỏi giỏ hàng ?');
        if (conf == true) {
            $.ajax({
                url: '/ClientCart/Delete',
                type: 'Post',
                data: { id: id },
                success: function (rs) {
                    if (rs.Success) {
                        $('#checkout-item').html(rs.Count);
                        $('#trow_' + id).remove();
                        LoadPageCart();
                    }
                }
            });
        }

    });
    $('body').on('click', '.btnUpdateQuantity', function (e) {
        e.preventDefault();
        var id = $(this).data('id');
        var quantity = parseInt($('#quantity_'+id).val());
        var conf = confirm('Bạn có chắc muốn Update sản phẩm này ?');
        if (conf == true) {
            Update(id, quantity);
        }

    });
});
function ShowCount() {
    $.ajax({
        url: '/ClientCart/ShowCount',
        type: 'GET',
        success: function (rs) {
            $('#checkout-item').html(rs.Count);
        }
    });
}

function Update(id, quantity) {
    $.ajax({
        url: '/ClientCart/UpdateQuantity',
        type: 'POST',
        data: { id: id, quantity: quantity },
        success: function (rs) {
/*            alert(rs.msg);*/
            LoadPageCart();
        }
    });
}
function LoadPageCart() {
    $.ajax({
        url: '/ClientCart/Partial_Item_Cart',
        type: 'GET',
        success: function (rs) {
            $('#load_cart').html(rs);
        }
    });
}
