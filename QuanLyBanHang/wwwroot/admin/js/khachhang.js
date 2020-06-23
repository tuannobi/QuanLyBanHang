//Update
function Update(ID) {
    $('.background-popup').show();
    $('.background-popup').addClass('d-flex align-items-center justify-content-center');
    $('.background-popup').load("/KhachHang/Edit/" + ID)

}

//delete
function Delete(ID) {

    var ans = confirm("Are you sure you want to delete this Record?");
    if (ans) {
        $.ajax({
            url: "/KhachHang/Delete/",

            type: "POST",
            // contentType: "application/json;charset=UTF-8",
            data: {
                id: ID
            },
            dataType: "text",
            success: function (result) {
                alert(result)
                setTimeout(function () {
                    location.reload();
                })

            },
            error: function (e) {
                alert("Error Found")
            }
        });
    }
}
//Detail
function Detail(ID) {
    $('.background-popup').show();
    $('.background-popup').addClass('d-flex align-items-center justify-content-center');
    $('.background-popup').load("/KhachHang/Details/" + ID)

}


$(document).ready(function () {
    $(document).on('click', '#add-custom', function () {
        $('.background-popup').show();
        $('.background-popup').addClass('d-flex align-items-center justify-content-center');
        $('.background-popup').load("/KhachHang/Create");
    })

    $(document).on('click', '#cancel-button', function () {
        $('.background-popup').hide();
        $('.background-popup').removeClass('d-flex align-items-center justify-content-center');
        $('.background-popup').empty();
    })

    $(document).on('submit', '#formthem', function (e) {
        var form = $('#formthem')[0];
        var data = new FormData(form);
        e.preventDefault();
        $.ajax({
            type: "POST",
            url: "KhachHang/Create/",
            enctype: 'multipart/form-data',
            data: data,
            // dataType: 'json',
            processData: false,
            contentType: false,
            cache: false,
            success: function (res) {
                setTimeout(function () {
                    location.reload();
                }, 1000)
                alert("Thành công")
            },
            error: function (e) {
                alert("Error Found")
            }

        })
    })

   


    //Delete
    $(function () {
        $('#checkall').change(function () {
            if ($(this).prop("checked") == true) {
                $("input[name=listcustom]").prop("checked", true);
            } else {
                $("input[name=listcustom]").prop("checked", false);
            }
        });
    })

    $("#delete").click(function () {
        var ans = confirm("Are you sure you want to delete selected product?");
        if (ans) {
            var selectedIDs = new Array();
            $('input:checkbox.tick').each(function () {
                if ($(this).prop("checked")) {
                    selectedIDs.push($(this).val());
                }
            });
            $.ajax({
                url: "/KhachHang/DeleteSelected/",
                type: "POST",
                dataType: "json",
                contextType: "application/json",
                data: {
                    selected: selectedIDs
                },
                //traditional: true,
                success: function (result) {
                    alert(result);
                    window.location.reload();
                    //window.location.href("/SanPham");
                },
                error: function (errormessage) {
                    alert(errormessage.responseText);
                }
            });


        }

    });

});