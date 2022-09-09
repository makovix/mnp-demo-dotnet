<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="mnp_demo_dotnet.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    Contact Details
    <br />
    <div id="contact-border" style="border:solid; margin: auto;">

        
        <table style="width:100%">
            <tr>
                <td class="table-td-contact" style="width: 84px">Name</td>
                <td class="table-td-contact"><asp:TextBox ID="txtName" runat="server" MaxLength="50"></asp:TextBox></td>
                <td style="width: 78px" class="table-td-contact-column-borders">Address</td>
                <td class="table-td-contact" style="width: 329px"><asp:TextBox ID="txtAddress" runat="server" MaxLength="50"></asp:TextBox></td>
                <td class="table-td-contact-column-borders" style="width: 136px">Last Date Contacted</td>
                <td class="table-td-contact"><asp:TextBox ID="txtLastDate" runat="server" Width="100" MaxLength="10"></asp:TextBox> YYYY-MM-DD</td>
            </tr>
            <tr>
                <td class="table-td-contact" style="width: 84px">Job Title</td>
                <td class="table-td-contact"><asp:TextBox ID="txtJobTitle" runat="server" MaxLength="50"></asp:TextBox></td>
                <td style="width: 78px"  class="table-td-contact-column-borders">Phone</td>
                <td class="table-td-contact" style="width: 329px"><asp:TextBox ID="txtPhone" runat="server" MaxLength="20"></asp:TextBox></td>
                <td class="table-td-contact-column-borders" style="width: 136px"></td>
                <td></td>
            </tr>
            <tr>
                <td class="table-td-contact" style="width: 84px">Company</td>
                <td class="table-td-contact"><asp:DropDownList ID="ddlCompany" runat="server"></asp:DropDownList></td>
                <td style="width: 78px"  class="table-td-contact-column-borders">Email</td>
                <td class="table-td-contact" style="width: 329px"><asp:TextBox ID="txtEmail" runat="server" MaxLength="50"></asp:TextBox></td>
                <td  class="table-td-contact-column-borders" style="width: 136px">Comments</td>
                <td class="table-td-contact"><asp:TextBox ID="txtComments" runat="server" Height="100%" Width="100%" MaxLength="300" TextMode="MultiLine"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="table-td-contact" style="width: 84px"></td>
                <td class="table-td-contact"><asp:TextBox ID="txtCID" runat="server" Visible="false"></asp:TextBox></td>
                <td style="width: 78px"  class="table-td-contact-column-borders"></td>
                <td class="table-td-contact" style="width: 329px"></td>
                <td  class="table-td-contact-column-borders" style="width: 136px"></td>
                <td class="table-td-contact"><asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" /> <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" /></td>
            </tr>
        </table>
        
    </div>
    
    <br />
    <br />
    <asp:Literal ID="litResponseMsg" runat="server" Text=""></asp:Literal>
</asp:Content>
