
<%@ Page Title="CS 491 Final" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebRole1._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron" style="text-align:center" >
        <h1 >CS 491 Cloudapp</h1>
            </div>

    <div class="row">
        <div class="col-md-2"></div>
        <div class="col-md-4">
   
           <asp:Panel id="Panel1" runat="server"
            BackColor="transparent">

               <h2>Upload a file:</h2>
               <div class ="well">

              
               <asp:FileUpload ID="FileUpload1" runat="server" />
           
              
               <br />

                <div class="btn-group">
                <asp:LinkButton ID="btnRandom" 
                      runat="server" 
                      CssClass="btn btn-success btn"    
                      OnClick="Button1_Click">
                     <i aria-hidden="true" class="glyphicon glyphicon-plus"></i>
               </asp:LinkButton>
               <asp:LinkButton ID="LinkButton1" 
                      runat="server" 
                      CssClass="btn btn-success btn"    
                      OnClick="public_Click">Public
               </asp:LinkButton>
               <asp:LinkButton ID="LinkButton2" 
                      runat="server" 
                      CssClass="btn btn-success btn"    
                      OnClick="private_Click">Unlisted
               </asp:LinkButton>
              
               </div>
                   <br />
                   <br />
                  
                </div>
                 
               
               <br />
                
                       </asp:Panel>
            <br />
        <asp:Literal ID="Literal1" runat="server"></asp:Literal>
        <br />
        <br />
            </div>
        
        <div class="col-md-4">
        <h2>Current Files:</h2> 
        <div class="well"> 
            
           
             <asp:ListBox ID="fileListBox" runat="server" Width="292px" 
                 OnSelectedIndexChanged="fileListBox_SelectedIndexChanged" 
                 Height="173px"
                EnableTheming ="true"
                 ForeColor ="DarkCyan" />

            <br />  <br />
             <div class="btn-group">
             <asp:TextBox ID="TextBox1" runat="server" Width="251px" Height="29px"></asp:TextBox>
               <asp:LinkButton ID="LinkButton7" 
                      runat="server" 
                      CssClass="btn btn-success btn-sm"    
                      OnClick="refreshButton">
                    <i aria-hidden="true" class="glyphicon glyphicon-refresh"></i>
               </asp:LinkButton>
                 </div>
            <br /> <br /> 
            <div class="btn-group">

            <div class="btn-group">
               <asp:LinkButton ID="LinkButton5" 
                      runat="server" 
                      CssClass="btn btn-success btn"    
                      OnClick="public_Click">Public
               </asp:LinkButton>
               <asp:LinkButton ID="LinkButton6" 
                      runat="server" 
                      CssClass="btn btn-success btn"    
                      OnClick="private_Click">Unlisted
               </asp:LinkButton>
                    </div>

            <div class="btn-group">

               <asp:LinkButton ID="LinkButton3" 
                      runat="server" 
                      CssClass="btn btn-info btn"    
                      OnClick="Button3_Click">
                    <i aria-hidden="true" class="glyphicon glyphicon-ok"></i>
               </asp:LinkButton>
               <asp:LinkButton ID="LinkButton4" 
                      runat="server" 
                      CssClass="btn btn-danger btn"    
                      OnClick="Button2_Click">
                    <i aria-hidden="true" class="glyphicon glyphicon-remove"></i>
               </asp:LinkButton>

                <br />
            </div>
             <br />
                </div>
            </div>

        </div>
             <br /> <br />
        <asp:Literal ID="uploadType" runat="server" Visible="False" Text="0"></asp:Literal>
        <asp:Literal ID="selects" runat="server" Visible="False" Text="asadfafd"></asp:Literal>
        <br /> <br /> <br /> 
        <br />
       </div>
        
     &nbsp;

</asp:Content>
