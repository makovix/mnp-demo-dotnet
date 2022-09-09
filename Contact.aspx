<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="mnp_demo_dotnet.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div id="contact-border" style="border:solid; margin: auto;">


        <table style="width:100%">
            <tr>
                <td style="width: 84px">Name</td>
                <td><asp:TextBox ID="txtName" runat="server"></asp:TextBox></td>
                <td style="width: 78px" class="table-column-borders">Address</td>
                <td style="width: 329px"><asp:TextBox ID="txtAddress" runat="server"></asp:TextBox></td>
                <td class="table-column-borders" style="width: 136px">Last Date Contacted</td>
                <td><asp:TextBox ID="txtLastDate" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width: 84px">Job Title</td>
                <td><asp:TextBox ID="txtJobTitle" runat="server"></asp:TextBox></td>
                <td style="width: 78px"  class="table-column-borders">Phone</td>
                <td style="width: 329px"><asp:TextBox ID="txtPhone" runat="server"></asp:TextBox></td>
                <td  class="table-column-borders" style="width: 136px"></td>
                <td></td>
            </tr>
            <tr>
                <td style="width: 84px">Company</td>
                <td><asp:DropDownList ID="ddlCompany" runat="server"></asp:DropDownList></td>
                <td style="width: 78px"  class="table-column-borders">Email</td>
                <td style="width: 329px"><asp:TextBox ID="txtEmail" runat="server"></asp:TextBox></td>
                <td  class="table-column-borders" style="width: 136px">Comments</td>
                <td><asp:TextBox ID="txtComments" runat="server" Height="100%" Width="100%"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width: 84px"></td>
                <td><asp:TextBox ID="txtCID" runat="server" Visible="false"></asp:TextBox></td>
                <td style="width: 78px"  class="table-column-borders"></td>
                <td style="width: 329px"></td>
                <td  class="table-column-borders" style="width: 136px"></td>
                <td><asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" /> <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" /></td>
            </tr>
        </table>
        
    </div>
    
    <br />
    <br />
    <asp:Literal ID="litResponseMsg" runat="server" Text=""></asp:Literal>
</asp:Content>
