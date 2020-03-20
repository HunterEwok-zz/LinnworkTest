<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Client._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript">
        var responseGetCategoriesVal = <%= "'" + responseGetCategoriesVal + "'"%>;
        var responseCreateCategoryVal = <%= "'" + responseCreateCategoryVal + "'"%>;
        var responseDeleteCategoryVal = <%= "'" + responseDeleteCategoryVal + "'"%>;
        var responseUpdateCategoryVal = <%= "'" + responseUpdateCategoryVal + "'"%>;
        var responseExecuteCustomScriptQueryVal = <%= "'" + responseExecuteCustomScriptQueryVal + "'"%>;

        $(document).ready(function () {
            document.getElementById("responseGetCategories").value = responseGetCategoriesVal;
            document.getElementById("responseCreateCategory").value = responseCreateCategoryVal;
            document.getElementById("responseDeleteCategory").value = responseDeleteCategoryVal;
            document.getElementById("responseUpdateCategory").value = responseUpdateCategoryVal;
            document.getElementById("responseExecuteCustomScriptQuery").value = responseExecuteCustomScriptQueryVal;

            $("#btnRequestGetCategories").click(function (event) {
                var params = '{}';
                makeRequestGetCategories(params);
            });

            $('#btnRequestCreateCategory').click(function (event) {
                var params = '{"categoryName": "' + document.getElementById('requestCreateCategory').value + '"}';
                makeRequestCreateCategory(params);
            });

            $('#btnRequestDeleteCategory').click(function (event) {
                var params = '{"categoryId": "' + document.getElementById('requestDeleteCategory').value + '"}';
                makeRequestDeleteCategory(params);
            });

            $('#btnRequestUpdateCategory').click(function (event) {
                var params = '{"categoryId": "' + document.getElementById('requestUpdateCategoryId').value +
                    '", "categoryName": "' + document.getElementById('requestUpdateCategoryName').value +
                    '"}';
                makeRequestUpdateCategory(params);
            });

            $('#btnRequestExecuteCustomScriptQuery').click(function (event) {
                var params = '{"script": "' + document.getElementById('requestExecuteCustomScriptQuery').value +
                    '"}';
                makeRequestExecuteCustomScriptQuer(params);
            });

            function makeRequestGetCategories(value) {
                var response = {};
                $.ajax({
                    type: "POST",
                    url: "/Default.aspx/GetCategories",
                    dataType: 'json',
                    data: value,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json"
                }).done(function (data) {
                    var json = $.parseJSON(data);
                    response = json;
                    location.reload();
                }).fail(function (jqXHR, textStatus, errorThrown) {
                    var json = $.parseJSON(data);
                    alert(json.error);
                    response = null;
                });
            }

            function makeRequestCreateCategory(value) {
                var response = {};
                $.ajax({
                    type: "POST",
                    url: "/Default.aspx/CreateCategory",
                    dataType: 'json',
                    data: value,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json"
                }).done(function (data) {
                    var json = $.parseJSON(data);
                    response = json;
                    location.reload();
                }).fail(function (jqXHR, textStatus, errorThrown) {
                    var json = $.parseJSON(data);
                    alert(json.error);
                    response = null;
                });
            }

            function makeRequestDeleteCategory(value) {
                var response = {};
                $.ajax({
                    type: "POST",
                    url: "/Default.aspx/DeleteCategory",
                    dataType: 'json',
                    data: value,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json"
                }).done(function (data) {
                    var json = $.parseJSON(data);
                    response = json;
                    location.reload();
                }).fail(function (jqXHR, textStatus, errorThrown) {
                    var json = $.parseJSON(data);
                    alert(json.error);
                    response = null;
                });
            }

            function makeRequestUpdateCategory(value) {
                var response = {};
                $.ajax({
                    type: "POST",
                    url: "/Default.aspx/UpdateCategory",
                    dataType: 'json',
                    data: value,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json"
                }).done(function (data) {
                    var json = $.parseJSON(data);
                    response = json;
                    location.reload();
                }).fail(function (jqXHR, textStatus, errorThrown) {
                    var json = $.parseJSON(data);
                    alert(json.error);
                    response = null;
                });
            }

            function makeRequestExecuteCustomScriptQuer(value) {
                var response = {};
                $.ajax({
                    type: "POST",
                    url: "/Default.aspx/ExecuteCustomScriptQuery",
                    dataType: 'json',
                    data: value,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json"
                }).done(function (data) {
                    var json = $.parseJSON(data);
                    response = json;
                    location.reload();
                }).fail(function (jqXHR, textStatus, errorThrown) {
                    var json = $.parseJSON(data);
                    alert(json.error);
                    response = null;
                });
            }
        });
    </script>

    <div class="container body-content">
        <button id="btnRefreshAnswers" name="btnRefreshAnswers">Refresh answers</button><br><br>

        <label for="requestGetCategories">Get categories</label>
        <button id="btnRequestGetCategories" name="btnRequestGetCategories">Send "Get categories"</button><br>
        <br>
        <label for="response">Response:</label><br>
        <br>
        <textarea id="responseGetCategories" name="responseGetCategories" readonly rows="5" cols="175"></textarea><br>
        <br>
        
        <label for="requestCreateCategory">Create category</label>
        <input type="text" id="requestCreateCategory" name="requestCreateCategory" placeholder="Category name">
        <button id="btnRequestCreateCategory" name="btnRequestCreateCategory">Send "Create category"</button><br>
        <br>
        <label for="response">Response:</label><br>
        <br>
        <textarea id="responseCreateCategory" name="responseCreateCategory" readonly rows="1" cols="175"></textarea><br>
        <br>

        <label for="requestDeleteCategory">Delete category</label>
        <input type="text" id="requestDeleteCategory" name="requestDeleteCategory" placeholder="Category ID">
        <button id="btnRequestDeleteCategory" name="btnRequestDeleteCategory">Send "Delete category"</button><br>
        <br>
        <label for="response">Response:</label><br>
        <br>
        <textarea id="responseDeleteCategory" name="responseDeleteCategory" readonly rows="1" cols="175"></textarea><br>
        <br>

        <label for="requestUpdateCategory">Update category</label>
        <input type="text" id="requestUpdateCategoryId" name="requestUpdateCategoryId" placeholder="Category ID">
        <input type="text" id="requestUpdateCategoryName" name="requestUpdateCategoryName" placeholder="New category name">
        <button id="btnRequestUpdateCategory" name="btnRequestUpdateCategory">Send "Update category"</button><br>
        <br>
        <label for="response">Response:</label><br>
        <br>
        <textarea id="responseUpdateCategory" name="responseUpdateCategory" readonly rows="1" cols="175"></textarea><br>
        <br>

        <label for="requestExecuteCustomScriptQuery">Execute Custom Script Query</label><br>
        <textarea id="requestExecuteCustomScriptQuery" name="requestExecuteCustomScriptQuery" rows="5" cols="175" placeholder="Query to execute"></textarea><br>
        <br>
        <button id="btnRequestExecuteCustomScriptQuery" name="btnRequestExecuteCustomScriptQuery">Send "Execute Custom Script Query"</button><br>
        <br>
        <label for="response">Response:</label><br>
        <br>
        <textarea id="responseExecuteCustomScriptQuery" name="responseExecuteCustomScriptQuery" readonly rows="5" cols="175"></textarea><br>
        <br>
    </div>
</asp:Content>
