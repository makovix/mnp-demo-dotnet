<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="mnp_demo_dotnet._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div>
        <asp:Button ID="btnNewContact" runat="server" Text="Create New Contact" OnClick="btnNewContact_Click" />
        <table id="contactsTable" style="border:solid ">
            <tr>
                <td>Name<br />(Job Title)</td>
                <td>Company</td>
                <td>Phone</td>
                <td>Address</td>
                <td>Email</td>
                <td>Last Date Contacted</td>
            </tr>
            <asp:PlaceHolder ID = "plhContactList" runat="server" />
            
        </table>

    </div>

</asp:Content>
