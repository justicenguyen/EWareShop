function themgiohang(idsp, soluong) {
    if (soluong == null) soluong = 1;
        $(document).ready(function () {
            $.ajax({
                url: '/GioHang/ThemGioHang',
                type: 'GET',
                data: { "idSP": idsp, "soLuong": soluong },
                success: function (c) {
                    alert(c);
                }
            });
        });
    }