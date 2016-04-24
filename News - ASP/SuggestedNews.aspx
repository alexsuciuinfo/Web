<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="SuggestedNews.aspx.cs" Inherits="_SuggestedNews" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">

    <asp:SqlDataSource ID="SqlDataSource1" runat="server" SelectCommand ="select n.Id as newsId, title,LEFT(heading,120) + '....' as heading,content,photo,date,name,c.id as Id from News n join Category c
on n.Category_id = c.Id where status = 'false' order by date desc" ConnectionString="<%$ ConnectionStrings:BazaDeStiriConnectionString %>"></asp:SqlDataSource>


          <asp:Repeater OnItemCommand="Repeater1_ItemCommand" ID="Repeater1" runat="server">
           
              <ItemTemplate>

                  <div class="col-md-4">
			<h2>
				<%#DataBinder.Eval(Container.DataItem, "Title")%>
			</h2>
			
			<img src = "<%#DataBinder.Eval(Container.DataItem, "Photo")%>" alt = "No photo"  style="float:left; margin : 1%; width:70px; height:70px"/> 
                <p><%#DataBinder.Eval(Container.DataItem, "Heading")%></p>			
			<p>
				<p class="article-meta"><strong style="color:red">Category :</strong><a class="btn" href='Category.aspx?category=<%#DataBinder.Eval(Container.DataItem, "Id")%>' style="color:green"><%#DataBinder.Eval(Container.DataItem, "name")%></a></p>
                <p class="article-meta"><strong style="color:red">Published :</strong> <%#DataBinder.Eval(Container.DataItem, "Date")%></p>
				<a class="btn" href='SuggestedNew.aspx?id=<%#DataBinder.Eval(Container.DataItem, "newsId")%>' style="color:blue">View details »</a>
                <div>
                    <asp:Button Text='<%#DataBinder.Eval(Container.DataItem,"newsId") %>' CommandName="Accept" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"newsId") %>'  style="border-radius:5px;width:30%;color:blue;"   runat="server" ID="ButtonAccept" Visible="true" ></asp:Button>
                    <asp:Button Text="Decline" CommandName="Decline" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"newsId") %>'  style="border-radius:5px;width:30%;color:blue;" runat="server" ID="ButtonDecline" Visible="true"></asp:Button>
                </div>
			</p>
		</div>
                 
              </ItemTemplate>
          </asp:Repeater>
                        
    <asp:Label ID="L1" runat="server" Text="LA"></asp:Label>

</asp:Content>

