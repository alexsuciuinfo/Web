<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Category.aspx.cs" Inherits="Category" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
    <section class="featured">
        <div class="content-wrapper">
            <hgroup class="title">
                <h1 style ="color:ivory; font-family:Batang; " id="category" runat="server"></h1>
            </hgroup>
        </div>
    </section>
</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">

    <asp:SqlDataSource ID="SqlDataSource1" runat="server" SelectCommand ="select Id as newsId, title,LEFT(heading,120) + '....' as heading,content,photo,date where status = 'true' and category_Id = -1 order by date desc" ConnectionString="<%$ ConnectionStrings:BazaDeStiriConnectionString %>"/>
    <p style="text-align:left; font-family:Cambria; font-weight:bold; font-size:20px;"> Sort by  
         
        <asp:DropDownList AutoPostBack="true" OnSelectedIndexChanged="Sort_select"  ID="DropDownList1" runat="server" style=" border-style:none;
    background-color:azure;
    border-width:0px;
    border: none;
    font-size:12pt;
    font-weight:bold;
    font-family:Verdana;
    OVERFLOW:auto;">
            <asp:ListItem Text ="Date Desc" Value ="date desc">

            </asp:ListItem>

            <asp:ListItem Text ="Date Asc" Value="date asc">

            </asp:ListItem>

            <asp:ListItem Text ="A-Z" Value ="title asc">

            </asp:ListItem>

            <asp:ListItem Text ="Z-A" Value ="title desc">

            </asp:ListItem>


        </asp:DropDownList></p>


    

          <asp:Repeater ID="Repeater1" runat="server" OnItemCommand="Repeater1_ItemCommand">
           
              <ItemTemplate>

                  <div class="col-md-4">
			<h2>
				<%#DataBinder.Eval(Container.DataItem, "Title")%>
			</h2>
			
			<img src = "<%#DataBinder.Eval(Container.DataItem, "Photo")%>" alt = "No photo"  style="float:left; margin : 1%; width:70px; height:70px"/> 
                <p><%#DataBinder.Eval(Container.DataItem, "Heading")%></p>			
			<p>
                <p class="article-meta"><strong>Published :</strong> <%#DataBinder.Eval(Container.DataItem, "Date")%></p>
				<a class="btn" href='New.aspx?id=<%#DataBinder.Eval(Container.DataItem, "newsId")%>' style="color:blue">View details »</a>
                <div>
                    <asp:Button Text="Edit" CommandName="Edit" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"newsId") %>'  style="border-radius:5px;width:30%;color:blue;"   runat="server" ID="ButtonEdit" Visible="false" ></asp:Button>
                    <asp:Button Text="Delete" CommandName="Delete" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"newsId") %>'  style="border-radius:5px;width:30%;color:blue;" runat="server" ID="ButtonDelete" Visible="false"></asp:Button>
                </div>
			</p>
		</div>
                 
              </ItemTemplate>
          </asp:Repeater>


</asp:Content>


