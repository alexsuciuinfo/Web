<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content runat="server" ID="FeaturedContent1" ContentPlaceHolderID="HeadContent">
    

</asp:Content>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
    <section class="featured">
        <div class="content-wrapper">
            <hgroup class="title">
                <h1 style ="color:ivory; font-family:Batang; " >Latest news</h1>
            </hgroup>
        </div>
    </section>
</asp:Content>





<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
   
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" SelectCommand ="select n.Id as newsId, title,LEFT(heading,120) + '....' as heading,content,photo,date,name,c.id as Id from News n join Category c
on n.Category_id = c.Id where status = 'true' order by date desc" ConnectionString="<%$ ConnectionStrings:BazaDeStiriConnectionString %>"></asp:SqlDataSource>


          <asp:Repeater OnItemDataBound="myfun" OnItemCommand="Repeater1_ItemCommand" ID="Repeater1" runat="server">
           
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
				<a class="btn" href='New.aspx?id=<%#DataBinder.Eval(Container.DataItem, "newsId")%>' style="color:blue">View details »</a>
                <div>
                    <asp:Button Text="Edit" CommandName="Edit" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"newsId") %>'  style="border-radius:5px;width:30%;color:blue;"   runat="server" ID="ButtonEdit" Visible="false" ></asp:Button>
                    <asp:Button Text="Delete" CommandName="Delete" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"newsId") %>'  style="border-radius:5px;width:30%;color:blue;" runat="server" ID="ButtonDelete" Visible="false"></asp:Button>
                </div>
                 <asp:Label runat="server" ForeColor="Red" ID="OptionsEd" Visible="false" />
			</p>
		</div>
                 
              </ItemTemplate>
          </asp:Repeater>

    <asp:Label runat="server" ID="LRaspuns1" />
                          
</asp:Content>
