
<%@ Page Title="CS 491 Final" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebRole1._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>CS 491 Cloud Share</h1>
            </div>

    <div class="row">

        <div class="col-md-4">
   
           <asp:Panel id="Panel1" runat="server"
            BackColor="transparent">

               <asp:CheckBoxList ID="CheckBoxList1" runat="server" OnSelectedIndexChanged="CheckBoxList1_SelectedIndexChanged">
                   <asp:ListItem Value="S">School</asp:ListItem>
                   <asp:ListItem Value="W">Work</asp:ListItem>
                   <asp:ListItem Value="F">Fun</asp:ListItem>
                   <asp:ListItem Value="O">Other</asp:ListItem>
                   <asp:ListItem Value="C">Clouds</asp:ListItem>
               </asp:CheckBoxList>

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
            <asp:CheckBoxList ID="CheckBoxList2" runat="server" OnSelectedIndexChanged="CheckBoxList2_SelectedIndexChanged">
                <asp:ListItem Value="S">School</asp:ListItem>
                <asp:ListItem Value="W">Work</asp:ListItem>
                <asp:ListItem Value="F">Fun</asp:ListItem>
                <asp:ListItem Value="O">Other</asp:ListItem>
                <asp:ListItem Value="C">Clouds</asp:ListItem>
            </asp:CheckBoxList>
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


                        <asp:LinkButton ID="btnRandom" 
                 runat="server" 
                 CssClass="btn btn-primary"    
                 OnClick="btnRandom_Click">
              <i aria-hidden="true" class="glyphicon glyphicon-download"></i>
                </asp:LinkButton>
        <br />
         <br />
         <br />
        <div class="col-md-4">
            <h2>Getting started</h2>
            <p>
                ASP.NET Web Forms lets you build dynamic websites using a familiar drag-and-drop, event-driven model.
            A design surface and hundreds of controls and components let you rapidly build sophisticated, powerful UI-driven sites with data access.
            </p>
            <p>
                <a class="btn btn-default" href="http://go.microsoft.com/fwlink/?LinkId=301948">Learn more &raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Get more libraries</h2>
            <p>
                NuGet is a free Visual Studio extension that makes it easy to add, remove, and update libraries and tools in Visual Studio projects.
            </p>
            <p>
                <a class="btn btn-default" href="http://go.microsoft.com/fwlink/?LinkId=301949">Learn more &raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Web Hosting</h2>
            <p>
                You can easily find a web hosting company that offers the right mix of features and price for your applications.
            </p>
            <p>
                <a class="btn btn-default" href="http://go.microsoft.com/fwlink/?LinkId=301950">Learn more &raquo;</a>
            </p>
        </div>
    </div>



</asp:Content>
