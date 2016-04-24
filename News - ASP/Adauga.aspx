<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Adauga.aspx.cs" Inherits="EditorPages_Adauga" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" Runat="Server">
   
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">

    <asp:SqlDataSource ID="SqlDataSource1" runat="server" SelectCommand="Select * from Category" 
        ConnectionString="<%$ ConnectionStrings:BazaDeStiriConnectionString %>">

    </asp:SqlDataSource>

     <asp:SqlDataSource ID="SqlDataSource2" runat="server" SelectCommand ="select title,heading as heading,content,photo,date,name,c.id as Id from News n join Category c
on n.Category_id = c.Id " ConnectionString="<%$ ConnectionStrings:BazaDeStiriConnectionString %>"></asp:SqlDataSource>


    <asp:Repeater runat="server" ID="Repeater1" DataSourceID="SqlDataSource2"><ItemTemplate>
    
<div class="form-style-2">
<div class="form-style-2-heading">Edit new</div>

<label for="field1"><span>Ttitle <span class="required">*</span></span><asp:TextBox Text='<%#Eval("title") %>' CausesValidation="true" ValidationGroup="AddNew" ID="TBTitle" runat="server"></asp:TextBox></label>
<label for="field2"><span>Heading <span class="required">*</span></span><asp:TextBox Text='<%#Eval("heading") %>' CausesValidation="true" ValidationGroup="AddNew" TextMode="MultiLine"   ID="TBHead" runat="server"></asp:TextBox></label>
<label for="field4"><span>Category</span>

    <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="SqlDataSource1" DataTextField="Name" DataValueField="Id"></asp:DropDownList >

</label>

  
    <label><span>Type<span class="required">*</span></span>
        <asp:DropDownList  ID="DropDownList2" runat="server" >
            <asp:ListItem Text="Original" Value="original" Selected="True"></asp:ListItem>
            <asp:ListItem Text="Other Source"></asp:ListItem>
        </asp:DropDownList> 
        </label>
     <label><span>Photo<span class="required">*</span></span><asp:FileUpload ID="FileUploadMI" runat="server"/></label>
<label for="field5"><span>Content <span class="required">*</span></span><asp:TextBox Text='<%#Eval("content") %>' CausesValidation="true" ValidationGroup="AddNew" ID="TBContinut" runat="server" TextMode ="MultiLine" Cssclass="textarea-field"></asp:TextBox></label>

<label><span>&nbsp;</span><asp:Button runat="server" OnClick="Button_EditNew" Text="Edit" CausesValidation="true" ValidationGroup="AddNew" /></label>


    <asp:RequiredFieldValidator ID="TitleRequired" runat="server" ControlToValidate="TBTitle"
                        ErrorMessage="Title is required." ToolTip="Title is required." ValidationGroup="AddNew"></asp:RequiredFieldValidator>

    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TBHead"
                        ErrorMessage="Heading is required." ToolTip="Heading is required." ValidationGroup="AddNew"></asp:RequiredFieldValidator>

    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TBContinut"
                        ErrorMessage="Content is required." ToolTip="Content is required." ValidationGroup="AddNew"></asp:RequiredFieldValidator>

   

        <asp:RegularExpressionValidator ValidationGroup="AddNew"
             ID="valPassword" runat="server"
   ControlToValidate="TBHead"     
   ErrorMessage="Minimum heading length is 200"
   ValidationExpression="^[\s\S]{200,}$" />
        </div>
    </ItemTemplate>
    </asp:Repeater>

    <asp:Literal ID="LRaspuns" runat="server"></asp:Literal>

</asp:Content>
