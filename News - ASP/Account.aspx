<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Account.aspx.cs" Inherits="Account" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" Runat="Server">

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">




    <script type="text/javascript">



        function suggest_New() {
            var selectBox = document.getElementById("<%= selectBox_Suggested.ClientID %>").value;
            if (selectBox.toString() == "original") {
                var cont = document.getElementById("spanType");
                cont.innerHTML = 'Content<span class="required">*</span>';

            }
            else {
                var cont = document.getElementById("spanType");
                cont.innerHTML = 'Website<span class="required">*</span>';

            }

        }

  </script>

        <asp:Label ForeColor="#666699" Font-Size="Larger" Font-Bold="true" runat="server" ID="LabelIsNotLoggedIn" Visible ="true">
            
        </asp:Label>

    <asp:Label ID="LabelIsLoggedIn" runat="server" Visible="true">

        <asp:SqlDataSource ID="SqlDataSource1" runat="server" SelectCommand="Select * from Category" 
        ConnectionString="<%$ ConnectionStrings:BazaDeStiriConnectionString %>">
    </asp:SqlDataSource>

       <asp:SqlDataSource ID="SqlDataSource2" runat="server" SelectCommand="Select FName,LName,BirthDate,Phone,Website,
allow = 
case AddOp
when 'True' then ' Add '
else ' '
end +
case EditOp
when 'True' then ' Edit '
else ' ' 
end +
case DeleteOp 
when 'True' then ' Delete '
else ' '
end +
case CommentOp 
when 'True' then 'Comment'
else ' '
end,
block = 
case AddOp
when 'False' then ' Add '
else ' '
end +
case EditOp
when 'False' then 'Edit '
else ' ' 
end +
case DeleteOp 
when 'False' then ' Delete '
else ' '
end +
case CommentOp 
when 'False' then ' Comment '
else ' '
end
           from UserProfileData p join Options o on p.UserId = o.UserId " 
        ConnectionString="<%$ ConnectionStrings:BazaDeStiriConnectionString %>">
    </asp:SqlDataSource>
        
    <%--Personl information --%>
    
    <div class="form-style-3" style="text-align:center;">

        
         
         <div class="form-style-2-heading">Personal information</div>  
             

    <asp:FormView ID="FormView" runat="server" DataSourceID="SqlDataSource2">
        <ItemTemplate>
            <fieldset><legend>Personal</legend>
                <div class="form-style-3" style="text-align:center;">
<label for="field1"><span>First Name</span><asp:Literal ID="Literal4" runat="server" Text='<%#Bind("FName") %>'></asp:Literal></label>
<label for="field2"><span>Last Name </span><asp:Literal ID="Literal3" runat="server" Text='<%#Bind("LName") %>'></asp:Literal></label>
<label for="field3"><span>Birth Date</span><asp:Literal ID="Literal2" runat="server" Text='<%#Bind("BirthDate") %>'></asp:Literal></label>
<label for="field4"><span>Phone</span><asp:Literal ID="Literal6" runat="server" Text='<%#Bind("Phone") %>' ></asp:Literal></label>
<label for="field5"><span>Website</span><asp:Literal ID="Literal5" runat="server" Text='<%#Bind("Website") %>'></asp:Literal></label>  
    <label for="field5"><span>Allowed : </span><asp:Literal ID="Literal1" runat="server" Text='<%#Bind("allow") %>'></asp:Literal></label>  
    <label for="field5"><span>Blocked :</span><asp:Literal ID="Literal7" runat="server" Text='<%#Bind("block") %>'></asp:Literal></label>    
  
                    </div>       
</fieldset>
            </ItemTemplate>
             </asp:FormView>

    
        <asp:Label runat ="server" ID ="LabelSuggestNew" Visible ="true">
             <%--Suggest a new --%>

             <div class="form-style-2-heading" id="divTextNew" runat="server">Suggest a new</div>  

<fieldset><legend>Message</legend>

    <label for="field1"><span>Ttitle <span class="required">*</span></span><asp:TextBox ID="TBTitle" runat="server" ></asp:TextBox>      

    </label>
<label for="field2"><span>Heading <span class="required">*</span></span><asp:TextBox  TextMode="MultiLine"   ID="TBHead" runat="server"></asp:TextBox></label>
<label for="field4"><span>Category</span>

    <asp:DropDownList ID="DropDownList1" runat="server"  DataTextField="Name" DataValueField="Id"></asp:DropDownList >

</label>
     <label><span>Photo<span class="required">*</span></span><asp:FileUpload ID="FileUploadMI" runat="server" /></label>
    <label><span>Type<span class="required">*</span></span>
        <select id="selectBox_Suggested" onchange="suggest_New();" runat="server">
        <option value="original" selected="selected">
            Original
        </option>
        <option value="other">
            Other Source
        </option>
                            </select></label>

<label><span id="spanType">Content <span class="required">*</span></span><asp:TextBox ID="TBContinut" runat="server" TextMode ="MultiLine" Cssclass="textarea-field"></asp:TextBox></label>

<label><span>&nbsp;</span><asp:Button PostBackUrl="~/Account.aspx" ID="Button_Suggest_New" OnClick="Button_SuggestNew" runat="server"  Text="Add"
    causevalidation="true" ValidationGroup="AddNew" /></label>
    <asp:Literal ID="LRaspuns" runat="server"></asp:Literal>

     
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

        
    <asp:Label ID="LRestrictUser" runat="server"></asp:Label>
</fieldset>

            </asp:Label>
             
         <%--Options --%>
      

        <asp:Label runat="server" ID="LabelOptions" Visible="true">

             <div class="form-style-2-heading">Options</div>  
             
            

<fieldset><legend>Options</legend>  

    <label for="field5"><span>Category Name<span class="required">*</span></span><asp:TextBox ID="TBAddCategory" 
        runat="server" Cssclass="textarea-field"></asp:TextBox></label>         
    <label><span>&nbsp;</span><asp:Button ID="Button_Add_Cattegory" OnClick="Button_AddCategory" runat="server"  
         CausesValidation="true" ValidationGroup="AddCategory"
        Text="Add Category" /></label>
             
<label for="field1"><span>Category Name<span class="required">*</span></span>
     <asp:DropDownList ID="DropDownList2" runat="server" DataTextField="Name" DataValueField="Id"></asp:DropDownList >

</label>         


    <label><span>&nbsp;</span><asp:Button ID="Button_Delete_Category" OnClick="Button_DeleteCategory" runat="server"  Text="Delete Category" /></label>

            
<label for="field1"><span>Category Name<span class="required">*</span></span>
     <asp:DropDownList ID="DropDownList3" runat="server" DataTextField="Name" DataValueField="Id"></asp:DropDownList >

</label>         

    <label for="field5"><span>New Category Name<span class="required">*</span></span><asp:TextBox ID="TBCategoryRename" 
        runat="server" Cssclass="textarea-field"></asp:TextBox>
    </label>  

        <label><span>&nbsp;</span><asp:Button ID="Button_Rename_Category" OnClick="Button_RenameCategory"  runat="server"  
             CausesValidation="true" ValidationGroup="RenameCategory"
            Text="Rename Category" /></label>

    <label><span>&nbsp;</span><asp:Button ID="ButtonViewNews"  OnClick="ButtonViewNews_Click" runat="server"  Text="View news" /></label>
        
            <asp:Label ID ="LabelAdmin" runat="server" Visible="true">
    <label><span>&nbsp;</span><asp:Button ID="ButtonAddEditor" OnClick="ButtonAddEditor_Click"  runat="server"  Text="Add editor" /></label>
    <label><span>&nbsp;</span><asp:Button ID="ButtonManageUsers" OnClick="ButtonManageUsers_Click" runat="server"  Text="Manage users" /></label>
    <label><span>&nbsp;</span><asp:Button ID="ButtonManageEditors" OnClick="ButtonManageEditors_Click" runat="server"  Text="Manage editors" /></label>

    </asp:Label>
    <asp:Literal Text="" runat="server" ID="LRaspuns1"></asp:Literal>

    
    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TBAddCategory"
                        ErrorMessage="Name can not be null." ToolTip="Name can not be null." ValidationGroup="AddCategory"></asp:RequiredFieldValidator>

       <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="TBCategoryRename"
                        ErrorMessage="Name can not be null." ToolTip="Name can not be null." ValidationGroup="RenameCategory"></asp:RequiredFieldValidator>
        

</fieldset>

              
            </asp:Label>
    </div>

    </asp:Label>
    
</asp:Content>

