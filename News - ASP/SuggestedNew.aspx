<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="SuggestedNew.aspx.cs" Inherits="_SuggestedNew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">

     <asp:SqlDataSource ID="SqlDataSource1" runat="server" SelectCommand ="select n.Id as newsId, c.Id, title,heading as heading,content,photo,date,name,c.id as Id from News n join Category c
on n.Category_id = c.Id where n.Id = -1" ConnectionString="<%$ ConnectionStrings:BazaDeStiriConnectionString %>"></asp:SqlDataSource>

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
                   <asp:Button Text="Accept" CommandName="Accept" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"newsId") %>'  style="border-radius:5px;width:10%;color:blue;"   runat="server" ID="ButtonEdit" Visible="true" ></asp:Button>
                    <asp:Button Text="Decline" CommandName="Decline" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"newsId") %>'  style="border-radius:5px;width:10%;color:blue;" runat="server" ID="ButtonDelete" Visible="true"></asp:Button>
                </div>
			</p>
		</div>

            </ItemTemplate>
          </asp:Repeater>
    <asp:Label ID="LRaspuns1" runat="server" Visible="false"></asp:Label>

</asp:Content>

