<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<TwitterCloud.WebApp.Models.AgregacaoDeTweets>>" %>
<% foreach (var item in Model)
   { %>
   <span style="font-size:<%: item.Importancia * 10 %>px"><%: item.Palavra %></span>&nbsp;
<% } %>
