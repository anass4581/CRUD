<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="WebApplication7.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .label-style {
            font-weight: bold;
            color: #333;
        }

        .container {
            margin: 20px;
        }

        .page-title {
            font-size: 24px;
            margin-bottom: 20px;
            color: #333;
        }

        .table-responsive {
            overflow-x: auto;
        }
    </style>

    <main>
        <div style="margin: 20px;">
            <h2 style="font-size: 24px; margin-bottom: 10px; text-align: center;">STUDENT MANAGEMENT</h2>
            <div style="margin-bottom: 20px;">
                <table>
                    <tr>
                        <td style="width: 150px;">
                            <asp:Label runat="server" Text="Student Id" ID="ctl04" CssClass="label-style"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                ControlToValidate="TextBox1"
                                ErrorMessage="Please enter a valid integer number."
                                ValidationExpression="^\d+$"
                                ForeColor="Red">
                            </asp:RegularExpressionValidator>
                        </td>
                        <td>
                            <%-- <asp:Button ID="Button5" runat="server" Text="All Records" OnClick="Button5_Click" CssClass="btn btn-primary" />--%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label runat="server" Text="Student Name" ID="ctl05" CssClass="label-style"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"
                                ControlToValidate="TextBox2"
                                 ErrorMessage="Please enter a valid name. It should start with an alphabet and can contain numbers."
                                ValidationExpression="^[A-Za-z][A-Za-z0-9\s]*$"
                                ForeColor="Red">
                            </asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label runat="server" Text="Address" ID="ctl06" CssClass="label-style"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox3" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server"
                                ControlToValidate="TextBox3"
                                ErrorMessage="Address should be between 10 and 50 characters long."
                                ValidationExpression="^.{10,50}$"
                                ForeColor="Red">
                            </asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label runat="server" Text="Age" ID="ctl07" CssClass="label-style"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox4" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server"
                                ControlToValidate="TextBox4"
                                ErrorMessage="Age must be a positive integer"
                                ValidationExpression="^(?=.*[1-9])\d+$"
                                ForeColor="Red">
                            </asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td colspan="2">
                            <%-- <asp:Button ID="Button1" runat="server" Text="Insert" OnClick="Button1_Click" CssClass="btn btn-success" />--%>
                            <asp:Button ID="Button2" runat="server" Text="Update" OnClick="Button2_Click" CssClass="btn btn-primary" />
                            <asp:Button ID="Button3" runat="server" Text="Delete" OnClick="Button3_Click" OnClientClick="return confirm('Are You Sure?')" CssClass="btn btn-danger" />
                            <%--<asp:Button ID="Button4" runat="server" Text="Search" OnClick="Button4_Click" CssClass="btn btn-info" />--%>
                            <asp:Button ID="ButtonClear" runat="server" Text="Clear All" OnClick="ButtonClear_Click" CssClass="btn btn-secondary" />
                        </td>
                    </tr>
                </table>
                <br />
                <div class="table-responsive">
                    <asp:GridView runat="server" ID="gridview2" CssClass="table table-striped table-bordered"></asp:GridView>
                </div>
            </div>
        </div>
    </main>
</asp:Content>
