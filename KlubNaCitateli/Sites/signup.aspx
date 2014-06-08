
<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="signup.aspx.cs" Inherits="KlubNaCitateli.Sites.signup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/signup.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="<%= Page.ResolveClientUrl("../Scripts/tabs-script.js") %>"></script>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
    <div class="main">
  <asp:Label ID="Label1" runat="server" Text="Sign Up" CssClass="signup"></asp:Label>
  <div id="tabs">
    <ul>
    <li id="li1" style="text-align: center; width: 100px">
    

    Profile
    </li>
    <li id="li2" style="text-align: center; width: 100px">
    
    About
    </li>
    </ul>
    <div id="div1">
        
        <div id="left">
        <asp:Image ID="Image1" runat="server" CssClass="Image1" Height="115px" 
            Width="100px" />
        <asp:Label ID="Label2" runat="server" CssClass="profilepicture" 
            Text="Choose profile picture"></asp:Label>
            <asp:FileUpload ID="profileImage" runat="server" CssClass="upload" />
       
        </div>
        <div id="right">
            
            
            
            
            
            
            
            <asp:Table ID="Table1" runat="server" CssClass="table" >
                <asp:TableRow ID="TableRow1" runat="server">
                    <asp:TableCell ID="TableCell1" runat="server" CssClass="table-first"><label>Username:</label></asp:TableCell>
                    <asp:TableCell runat="server">
                        <asp:TextBox ID="username" runat="server" CssClass="table-second"></asp:TextBox><br />
                       <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="username" ErrorMessage="You must enter a username!"></asp:RequiredFieldValidator>
                   </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow ID="TableRow2" runat="server">
                    <asp:TableCell ID="TableCell3" runat="server" CssClass="table-first"><label>Password:</label></asp:TableCell>
                    <asp:TableCell ID="TableCell4" runat="server"><asp:TextBox ID="password" runat="server" CssClass="table-second"></asp:TextBox>
                 <br /> <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="You must enter a password!" ControlToValidate="password">
                    </asp:RequiredFieldValidator></asp:TableCell>
                    
                </asp:TableRow>
                <asp:TableRow ID="TableRow3" runat="server">
                    <asp:TableCell ID="TableCell5" runat="server" CssClass="table-first"><label>Retype password:</label></asp:TableCell>
                    <asp:TableCell ID="TableCell6" runat="server"><asp:TextBox ID="repassword" runat="server" CssClass="table-second"></asp:TextBox>
                    <br /> <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Password doesn't match!" ControlToCompare="password" ControlToValidate="repassword"></asp:CompareValidator>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow ID="TableRow4" runat="server">
                    <asp:TableCell ID="TableCell7" CssClass="table-first" runat="server"><label>Name:</label></asp:TableCell>
                    <asp:TableCell ID="TableCell8" runat="server"><asp:TextBox ID="name" runat="server" CssClass="table-second"></asp:TextBox></asp:TableCell>
                   
                </asp:TableRow>
                <asp:TableRow ID="TableRow5" runat="server">
                    <asp:TableCell ID="TableCell9" CssClass="table-first" runat="server"><label>Surname:</label></asp:TableCell>
                    <asp:TableCell ID="TableCell10" runat="server"><asp:TextBox ID="surname" runat="server" CssClass="table-second"></asp:TextBox></asp:TableCell>
                  
                </asp:TableRow>
                <asp:TableRow ID="TableRow6" runat="server">
                    <asp:TableCell ID="TableCell11" CssClass="table-first" runat="server"><label>Email:</label></asp:TableCell>
                    <asp:TableCell ID="TableCell12" runat="server"><asp:TextBox ID="email" runat="server" CssClass="table-second"></asp:TextBox>
                <br /> <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="email" ErrorMessage="Incorrect format!" ValidationExpression="^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"></asp:RegularExpressionValidator> </asp:TableCell>    
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
    <div id="categories-divs">
        <asp:Label ID="Label4" runat="server" Text="Label" CssClass="label3">About yourself:</asp:Label>
        <asp:TextBox ID="TextBox2" runat="server" CssClass="aboutTxt"></asp:TextBox>

    </div>
    <div id="backFinishButtons"> 
  
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always" >
        <ContentTemplate>
            <asp:Button ID="Button2" runat="server" Text="Back" CssClass="backBtn" CausesValidation="false" />
           
            <asp:Button ID="Button4" runat="server" CssClass="finishBtn" Text="Finish" 
                OnClick="finishButton_click" />
         </ContentTemplate>
    </asp:UpdatePanel>
     <asp:Label ID="finishLabel" runat="server" CssClass="checkUsernameEmail"></asp:Label>
    </div>
    
    </div>
        
    </div>
    </div>  
</asp:Content>
