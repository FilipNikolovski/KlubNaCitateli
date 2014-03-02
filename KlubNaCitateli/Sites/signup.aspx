<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="signup.aspx.cs" Inherits="KlubNaCitateli.Sites.signup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<link href="../Styles/signup.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="<%= Page.ResolveClientUrl("../Scripts/tabs-script.js") %>"></script>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <br />
    <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Label ID="Label1" runat="server" Text="Sign Up" CssClass="signup"></asp:Label>
&nbsp;<br />
    <br />
    <br />
    <br />
    <ul>
    <li id="li1" style="text-align: center; width: 100px">
    &nbsp;&nbsp;&nbsp;
    Profile&nbsp;&nbsp;&nbsp;
    </li>
    <li id="li2" style="text-align: center; width: 100px">
    &nbsp;&nbsp;&nbsp;&nbsp;
    About&nbsp;&nbsp;&nbsp;&nbsp;
    </li>
    </ul>
    <div id="div1">
        <br />
        <div id="left">
        <asp:Image ID="Image1" runat="server" CssClass="Image1" Height="115px" 
            Width="100px" />
        &nbsp;&nbsp;&nbsp;
        <br />
        <asp:Label ID="Label2" runat="server" CssClass="profilepicture" 
            Text="Profile picture"></asp:Label>
        <br />
        <asp:Button ID="Button1" runat="server" CssClass="browse" Text="Browse" />
        <br />
        </div>
        <div id="right">
            
            <asp:Table ID="Table1" runat="server" CssClass="table" Height="47px" 
                style="margin-left: 24px" Width="658px">
                <asp:TableRow runat="server">
                    <asp:TableCell runat="server" class="table-first"><label>Username:</label></asp:TableCell>
                    <asp:TableCell runat="server">
                        <asp:TextBox ID="username" runat="server" class="table-second"></asp:TextBox></asp:TableCell>
                   
                </asp:TableRow>
                <asp:TableRow runat="server">
                    <asp:TableCell runat="server" class="table-first"><label>Password:</label></asp:TableCell>
                    <asp:TableCell runat="server"><asp:TextBox ID="password" runat="server" class="table-second"></asp:TextBox></asp:TableCell>
                    
                </asp:TableRow>
                <asp:TableRow runat="server">
                    <asp:TableCell runat="server" class="table-first"><label>Retype password:</label></asp:TableCell>
                    <asp:TableCell runat="server"><asp:TextBox ID="repassword" runat="server" class="table-second"></asp:TextBox></asp:TableCell>
                    
                </asp:TableRow>
                <asp:TableRow runat="server">
                    <asp:TableCell class="table-first" runat="server"><label>Name:</label></asp:TableCell>
                    <asp:TableCell runat="server"><asp:TextBox ID="name" runat="server" class="table-second"></asp:TextBox></asp:TableCell>
                   
                </asp:TableRow>
                <asp:TableRow runat="server">
                    <asp:TableCell class="table-first" runat="server"><label>Surname:</label></asp:TableCell>
                    <asp:TableCell runat="server"><asp:TextBox ID="surname" runat="server" class="table-second"></asp:TextBox></asp:TableCell>
                  
                </asp:TableRow>
                <asp:TableRow runat="server">
                    <asp:TableCell class="table-first" runat="server"><label>Email:</label></asp:TableCell>
                    <asp:TableCell runat="server"><asp:TextBox ID="email" runat="server" class="table-second"></asp:TextBox></asp:TableCell>
                    
                </asp:TableRow>
            </asp:Table> 
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            
    </asp:ScriptManager>
           
           <script type="text/javascript">
               Sys.WebForms.PageRequestManager.getInstance().add_endRequest(GoToAbout);
    </script>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always" >
    <ContentTemplate>
        <asp:Button ID="Button3" runat="server" CssClass="btn" CausesValidation="false" Text="Next" />
     </ContentTemplate>
    </asp:UpdatePanel>
    </div>
    </div>
    
    <div id="div2">
    
    <div id="categories-div">
        <asp:Label ID="Label3" runat="server" Text="Label" CssClass="label3">Fields of interest:</asp:Label>
        <asp:TextBox ID="TextBox1" runat="server" CssClass="autoCompleteSearch"></asp:TextBox>
    </div>
    <div id="backFinishButtons"> 
  
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always" >
        <ContentTemplate>
            <asp:Button ID="Button2" runat="server" Text="Back" CssClass="backBtn" CausesValidation="false" />
            <asp:Button ID="Button4" runat="server" CssClass="finishBtn" Text="Finish" />
         </ContentTemplate>
    </asp:UpdatePanel>
    </div>
    
    </div>
        
    <br />
</asp:Content>
