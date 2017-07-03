
$('.daduyet').off('click').on('click', function () {
    var iddh = $(this).data('iddh');
    $.ajax(
        {
            type: 'GET',
            url: "/DonHang/ThayDoiDaDuyet",
            data: { "iddh": iddh },
            success: function (rs) {
                alert(rs);
            }
        })
});

$('.dagiao').off('click').on('click', function () {
    var iddh = $(this).data('iddh');
    $.ajax(
        {
            type: 'GET',
            url: "/DonHang/ThayDoiDaGiao",
            data: { "iddh": iddh },
            success: function (rs) {
                alert(rs);
            }
        })
});

