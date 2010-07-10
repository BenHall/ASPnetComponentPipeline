<%@ Import Namespace="ASPnetComponentPipeline.Extensions" %>
<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ASPnetComponentPipeline.ViewModels.HomeViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Home Page
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    Component Data: <h2><%= Html.Encode(Model.Component_Title) %></h2>
    View Model Data: <h2><%= Html.Encode(Model.Message) %></h2>
    View Model Data: <h2><%= Html.Encode(ViewData.MasterPageModel().SiteTitle)%></h2>
</asp:Content>
