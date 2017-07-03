//Xóa 1 sản phẩm trong giỏ hàng
function xoaSP(idsp) {
    $(document).ready(function () {
        $.ajax({
            url: '/Giohang/XoaGioHang',
            type: 'GET',
            data: { "idSP": idsp },
            success: function (rs) {
                $('#spgh_' + idsp).remove();
                var tongtientatca = rs.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,");
                $('#tongtatca').html(tongtientatca);
            }
        });
    });
}
// Cập nhật giỏ hàng và tính tổng tiền sản phẩm vừa cập nhật
function tongtien(idsp) {
    var soLuongCu = $('#soluong_' + idsp).val();
    var giaBan = $('#gia_' + idsp).val();
    var soLuongMoi = $('#sl_' + idsp).val();
    var giaSauCapNhat = $('#sl_' + idsp).val() * giaBan;
    var soLuongCapNhat = soLuongMoi - soLuongCu;
    $(document).ready(function () {
        $.ajax({
            url: '/Giohang/SuaGioHang',
            type: 'GET',
            data: { "idSP": idsp, "soLuong": soLuongCapNhat },
            success: function (rs) {
                $('#tongtien_' + idsp).html(giaSauCapNhat.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,") + " đ");
                $('#soluong_' + idsp).val(soLuongMoi);
                var tongtientatca = rs.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,");
                $('#tongtatca').html(tongtientatca);
            }
        });
    });
}