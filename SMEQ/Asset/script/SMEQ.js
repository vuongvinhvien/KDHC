        //Checkbox

        //select ui bombobox
        $(".District").chosen();
        $(".Province").chosen();
        //table phuong tien do khách hàng
        $(document).ready(function () {
            var i = 1;
            $("#add_row").click(function () {
                $('#addr' + i).html("<td>" + (i + 1) + "</td><td><input name='phương tiện đo" + i + "' type='text' placeholder='phương tiện đo' class='form-control input-md'  /> </td><td><input  name='số lượng" + i + "' type='text' placeholder='số lượng'  class='form-control input-md'></td><td><input  name='phương pháp thử" + i + "' type='text' placeholder='phương pháp thử'  class='form-control input-md'></td><td><input  name='kiemdinh" + i + "' type='text' placeholder='kiểm định'  class='form-control input-md'></td><td><input  name='hiệu chuẩn" + i + "' type='text' placeholder='hiệu chuẩn'  class='form-control input-md'></td>");

                $('#tab_logic').append('<tr id="addr' + (i + 1) + '"></tr>');
                i++;
            });
            $("#delete_row").click(function () {
                if (i > 1) {
                    $("#addr" + (i - 1)).html('');
                    i--;
                }
            });

        });
        //table TTKT
        $(document).ready(function () {
            var i = 1;
            $("#add_row2").click(function () {
                $('#addr2' + i).html("<td>" + (i + 1) + "</td><td><input name='phương tiện đo" + i + "' type='text' placeholder='phương tiện đo' class='form-control input-md'  /> </td><td><input  name='số lượng" + i + "' type='text' placeholder='số lượng'  class='form-control input-md'></td><td><input  name='đơn giá" + i + "' type='text' placeholder='đơn giá'  class='form-control input-md'></td><td><input  name='tăng" + i + "' type='text' placeholder='tăng'  class='form-control input-md'></td><td><input  name='giảm" + i + "' type='text' placeholder='giảm'  class='form-control input-md'></td><td><input  name='thành tiền" + i + "' type='text' placeholder='thành tiền'  class='form-control input-md'></td>");

                $('#tab_logic2').append('<tr id="addr2' + (i + 1) + '"></tr>');
                i++;
            });
            $("#delete_row2").click(function () {
                if (i > 1) {
                    $("#addr2" + (i - 1)).html('');
                    i--;
                }
            });

        });