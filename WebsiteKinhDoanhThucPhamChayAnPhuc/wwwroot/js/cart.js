var cart = {
    init: function () {
        cart.regEvents();
    },
    regEvents: function () {
        $('#btnContinue').off('click').on('click', function () {
            window.location.href = "/san-pham";
        });
        $('#btnPayment').off('click').on('click', function () {
            window.location.href = "/thanh-toan";
        });
        $('#btnUpdate').off('click').on('click', function () {
            var listProduct = $('.txtQuantity');
            var cartList = [];
            $.each(listProduct, function (i, item) {
                cartList.push({
                    Quantity: $(item).val(),
                    Product: {
                        IdPro: $(item).data('id')
                    }
                });
            });
            $.ajax({
                url: '/Cart/Update',
                data: { cartModel: JSON.stringify(cartList) },
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = "/gio-hang";
                    }
                }
            })
        });
        $('#btnDeleteAll').off('click').on('click', function () {
            $.ajax({
                url: '/Cart/DeleteAll',
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = "/gio-hang";
                    }
                }
            })
        });
        $('.btn-delete').off('click').on('click', function (e) {
            e.preventDefault();
            $.ajax({
                data: { id: $(this).data('id') },
                url: '/Cart/Delete',
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        $(`.product-item[data-id="${$(this).data('id')}"]`).remove();
                        window.location.href = "/gio-hang";
                    }
                }
            })
        });

        $('.btn-minus').off('click').on('click', function (e) {
            e.preventDefault();

            var productId = $(this).data('id');
            var quantityInput = $(this).siblings('.txtQuantity');
            var currentQuantity = parseInt(quantityInput.val());

            if (currentQuantity === 1) {
                if (confirm("Bạn có muốn xóa sản phẩm này khỏi giỏ hàng?")) {
                    cart.removeItem(productId);
                }
            } else {
                currentQuantity--;
                quantityInput.val(currentQuantity);
                cart.updateCart(productId, currentQuantity);
            }
        });

        $('.btn-plus').off('click').on('click', function (e) {
            e.preventDefault();

            var productId = $(this).data('id');
            var quantityInput = $(this).siblings('.txtQuantity');
            var currentQuantity = parseInt(quantityInput.val());

            currentQuantity++;
            quantityInput.val(currentQuantity);
            cart.updateCart(productId, currentQuantity);
        });
    },
   
    updateCart: function (productId, newQuantity) {
        var listProduct = $('.txtQuantity');
        var cartList = [];
        $.each(listProduct, function (i, item) {
            cartList.push({
                Quantity: $(item).val(),
                Product: {
                    IdPro: $(item).data('id')
                }
            });
        });
        $.ajax({
            url: '/Cart/Update',
            data: { cartModel: JSON.stringify(cartList) },
            dataType: 'json',
            type: 'POST',
            success: function (res) {
                if (res.status == true) {
                    window.location.href = "/gio-hang";
                }
            }
        })
    },
    removeItem: function (productId) {
        $.ajax({
            url: '/Cart/Delete',
            data: { id: productId },
            dataType: 'json',
            type: 'POST',
            success: function (res) {
                if (res.status == true) {
                    $(`.product-item[data-id="${productId}"]`).remove();
                    window.location.href = "/gio-hang";
                }
            }
        });
    },


    
};

cart.init();
