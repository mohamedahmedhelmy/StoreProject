let ClsCategory =
{
    LoadCategories: function () {
        Helper.AjaxCallGet("/api/CategoryApi", {}, "json", function (data) {
            let htmlData = "";
            for (let i = 0; i < data.length; i++) {
                htmlData = htmlData + ClsCategory.Template1(data[i]);
            }
            $("#Categories").html(htmlData);
        },
            function () {

            });
    },
    Template1: function (cat) {
        let cattegory = "<div class='collection-brand-filter'>";
        cattegory = cattegory +"<div class='custom-control custom-checkbox collection-filter-checkbox'>"
        cattegory = cattegory + "<ul><li><a onclick='ClsItems.FilterItems(" + cat.categoryId + ")'>" + cat.categoryName + " </a></li></ul>"
        cattegory = cattegory + "</div></div>"
        return cattegory;
    }
}
ClsCategory.LoadCategories();
