
<%@ Page Title="CS 491 Final" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebRole1._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>CS 491 Cloud Share</h1>
            </div>

    <div class="row">

        <div class="col-md-4">
   
           <asp:Panel id="Panel1" runat="server"
            BackColor="transparent">

               <br />
                    <asp:FileUpload ID="FileUpload1" runat="server" OnClick="FileUpload1_Click"/>
                     &nbsp;
                     <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Submit" />
                     <br /><br /><br />

                       </asp:Panel>
            <br />
        <asp:Literal ID="Literal1" runat="server"></asp:Literal>
        <br />
        <br />
        <h2>Current Files:</h2> <br />
        <div class="well"> 
            <asp:TextBox ID="TextBox1" runat="server" OnTextChanged="TextBox1_TextChanged"></asp:TextBox>
            <br />

            <div class="btn-group btn-group-vertical">

             <asp:Button ID="Button2" runat="server" Text="Delete" OnClick="Button2_Click" Width="94px"/>
                <br />
             <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="Download" />
            </div>
            
           
             <asp:ListBox ID="fileListBox" runat="server" Width="292px" OnSelectedIndexChanged="fileListBox_SelectedIndexChanged" Height="76px" />

            </div>
        </div>
        <br />
        <br />
       </div>
    <div class="row">


        <div class="alert alert-dismissable alert-success">
          <button type="button" class="close" data-dismiss="alert">×</button>
             <strong>Well done!</strong> You successfully read <a href="#" class="alert-link">this important alert message</a>.
        </div>

                        <asp:LinkButton ID="btnRandom" 
                 runat="server" 
                 CssClass="btn btn-primary"    
                 OnClick="btnRandom_Click">
              <i aria-hidden="true" class="glyphicon glyphicon-download"></i>
                </asp:LinkButton>
        <br />
         <br />
         <br />
    </div>



</asp:Content>
