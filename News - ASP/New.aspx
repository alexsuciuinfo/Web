<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="New.aspx.cs" Inherits="New" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
    <section class="featured">
        <div class="content-wrapper">
            <hgroup class="title">
                <h1 style ="color:red" >Latest news</h1>
            </hgroup>
        </div>
    </section>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">

    <asp:SqlDataSource ID="SqlDataSource1" runat="server" SelectCommand ="select n.Id as newsId, c.Id, title,heading as heading,content,photo,date,name,c.id as Id from News n join Category c
on n.Category_id = c.Id where n.Id = -1" ConnectionString="<%$ ConnectionStrings:BazaDeStiriConnectionString %>"></asp:SqlDataSource>

     <asp:SqlDataSource ID="SqlDataSource2" runat="server" SelectCommand ="select n.Id as newsID, c.Id as com_Id,c.date as date, c.comment as comm,
         u.UserName as user_name from Comments c join News n on c.News_Id = n.Id join Users u on u.UserId = c.User_Id where n.Id = -1" ConnectionString="<%$ ConnectionStrings:BazaDeStiriConnectionString %>"></asp:SqlDataSource>


    <asp:Repeater runat="server" ID="Repeater1" OnItemCommand="Repeater1_ItemCommand">

        <ItemTemplate>
                  <div class="col-md-10">
			<h2>
				<%#Eval("Title")%>
			</h2>
			
			<img src = "<%#Eval("Photo")%>" alt = "No photo"  style="float:left; margin : 1%; width:200px; height:200px"/> 
                <p style="font-family:Calibri;font-weight:bold;font-size:20px"><%#Eval("Heading")%></p>	
                      <p><%#Eval("Content")%></p>		
			<p>
				<p class="article-meta"><strong style="color:red">Category :</strong><a class="btn" href='Category.aspx?category=<%#Eval("Id")%>' style="color:green"><%#Eval("name")%></a></p>
                <p class="article-meta"><strong style="color:red">Published :</strong> <%#Eval("Date")%></p>
				
                <div>
                   <asp:Button Text="Edit" CommandName="Edit" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"newsId") %>'  style="border-radius:5px;width:10%;color:blue;"   runat="server" ID="ButtonEdit" Visible="false" ></asp:Button>
                    <asp:Button Text="Delete" CommandName="Delete" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"newsId") %>'  style="border-radius:5px;width:10%;color:blue;" runat="server" ID="ButtonDelete" Visible="false"></asp:Button>
                </div>
                <asp:Label runat="server" ForeColor="Red" ID="OptionsEd" Visible="false" />
			</p>
		</div>

            </ItemTemplate>
          </asp:Repeater>

    <div class="row">
<div class="col-sm-11">
<div class="form-style-2-heading">Comments</div>
</div>
</div>

    
    <asp:Repeater ID="Repeater2" runat ="server" OnItemCommand="Repeater2_ItemCommand">
        <ItemTemplate>
    <div class="container">
<div class="row">
    <div class="col-sm-12">
<div class="col-sm-1">
<div class="thumbnail">
<img class="img-responsive user-photo" src="avatar.png">
</div>
</div>

<div class="col-sm-10">
<div class="panel panel-default">
<div class="panel-heading">
<strong><%#Eval("user_name") %></strong> <span class="text-muted"><%#Eval("date") %></span><span><asp:Button Text="Delete" CommandName="DeleteComm" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"com_Id") %>'
       style="border-radius:5px;width:10%;color:blue; margin-left:5%;"   runat="server" ID="ButtonDelComm" Visible="false" ></asp:Button>
</span>
</div>
<div class="panel-body">
<%#Eval("comm") %>
</div>
</div>
</div>
        </div>
    </div>
</div>
        </ItemTemplate>
       </asp:Repeater>


    <div class="form-style-2">

    <div class="form-style-2-heading">Add a comment</div>

<fieldset><legend>Message</legend>
   
<label for="field5"><span>Message</span><asp:TextBox ID="TBContinut" runat="server" TextMode ="MultiLine" Cssclass="textarea-field"></asp:TextBox></label>

<label><span>&nbsp;</span><asp:Button ID="Button1" runat="server"  Text="Add" OnClick="Button_Add_Click" /></label>
    <asp:Literal ID="LRaspuns1" runat="server" Visible ="false"></asp:Literal>
</fieldset>

        </div>

</asp:Content>

