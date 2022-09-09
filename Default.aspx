<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="mnp_demo_dotnet._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div style="margin: auto;">
        <asp:Button ID="btnNewContact" runat="server" Text="Create New Contact" OnClick="btnNewContact_Click" />
        <br /><br />
        <table id="contactsTable" style="border:solid; width:100% ">
            <tr>
                <td class="table-td-header-contactlist">Name<br />(Job Title)</td>
                <td class="table-td-header-contactlist">Company</td>
                <td class="table-td-header-contactlist">Phone</td>
                <td class="table-td-header-contactlist">Address</td>
                <td class="table-td-header-contactlist">Email</td>
                <td class="table-td-header-contactlist">Last Date Contacted</td>
            </tr>
            <asp:PlaceHolder ID = "plhContactList" runat="server" />
            
        </table>

    </div>

</asp:Content>
