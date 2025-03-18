<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Leave.aspx.cs" Inherits="LeaveManagement.Leave" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Leave Apply</title>
        <script src="https://code.jquery.com/jquery-3.7.1.js"></script>
    <script>
        $(document).ready(function () {
            alert("hello");
            $("#<%=ddlEmployees.ClientID%>").change(function () {
                var empid = $(this).val();
                alert(empid);
                $.ajax({
                    type: "POST",
                    url: "Leave.aspx/GetLeaveTypes",
                    data: JSON.stringify({ id: empid }),
                    contentType: "application/json",
                    dataType: "json",
                    success: function (response) {
                        var options = '<option value="">Select Leave</option>';
                        $.each(response.d, function (index, item) {
                            options += '<option value="' + item.leave_id + '">' + item.leave_name + '</option>';
                        });
                        $("#<%= ddlLeaveType.ClientID %>").html(options);
                    },
                    error: function (xhr, status, error) {
                        console.log("AJAX Error:" + error);
                    }
                });
            });
        });
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Leave Apply</h2>
            Employee Name:<asp:DropDownList ID="ddlEmployees" runat="server"></asp:DropDownList><br />
            Leave Apply:<asp:DropDownList ID="ddlLeaveType" runat="server"></asp:DropDownList><br />
            No.of Leave:<asp:TextBox ID="ddlNoOfLeave" runat="server"></asp:TextBox><br />
            <asp:Button ID="Button1" runat="server" Text="Submit" OnClick="Button1_Click" />
        </div>
    </form>
</body>
</html>
