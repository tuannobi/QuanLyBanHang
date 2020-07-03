﻿

//delete
function Delete(ID) {

    var ans = confirm("Are you sure you want to delete this Record?");
    if (ans) {
        $.ajax({
            url: "/Admin/Delete/",

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



$(document).ready(function () {
   

    $(document).on('submit', '#formthem', function (e) {
        var form = $('#formthem')[0];
        var data = new FormData(form);
        e.preventDefault();
        $.ajax({
            type: "POST",
            url: window.location + "/Create",
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
                $("input[name=listadmin]").prop("checked", true);
            } else {
                $("input[name=listadmin]").prop("checked", false);
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
                url: "/Admin/DeleteSelected/",
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