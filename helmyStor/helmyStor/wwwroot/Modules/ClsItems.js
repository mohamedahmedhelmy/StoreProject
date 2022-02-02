let Items = [];

let ClsItems = {
    LoadItems: function () {
        Helper.AjaxCallGet("/api/ProductsApi/GetAllProducts", {}, "json", function (data) {
            console.log(data);
            Items = data;
            console.log(data);
            let htmlData = "";
            for (let i = 0; i < data.length; i++) {
                htmlData = htmlData + ClsItems.Template1(data[i]);
            }
            $("#DivTemplate1").html(htmlData);
        },
            function () {

            });
    },
    Template1: function (item) {
        let itemHtml = " <div class='col-xl-3 col-6 col-grid-box'>";
        itemHtml = itemHtml + "<div class='product-box'>";
        itemHtml = itemHtml + "<div class='img-wrapper'>";
        itemHtml = itemHtml + "<div class='front'><a href='/Products/Details/" + item.productId + "'><img src='/Uploads/" + item.imageName +"' class='img-fluid blur-up lazyload bg-img' alt=''></a></div>";
        itemHtml = itemHtml + "<div class='back'><a href='/Products/Details/" + item.productId + "'><img src='/Uploads/" + item.imageName +"' class='img-fluid blur-up lazyload bg-img' alt=''></a></div>";
        itemHtml = itemHtml + "</div>";
        itemHtml = itemHtml + "<div class='product-detail'>";
        itemHtml = itemHtml + "<div>";
        itemHtml = itemHtml + "<div class='rating''><i class='fa fa-star'></i> <i class='fa fa-star'></i> <i class='fa fa-star'></i> <i class='fa fa-star'></i> <i class='fa fa-star'></i></div>";
        itemHtml = itemHtml + "<a href='/Products/Details/'" + item.productId + "'> <h6>"+item.productName+"</h6></a>";
        itemHtml = itemHtml + "<h4>" + item.salesPrice+"</h4>";
        itemHtml = itemHtml + "</div></div></div></div>";

        return itemHtml;
    }
    ,
    FilterItems: function (catId) {
        let newItems = $.grep(Items, function (n, i) { // just use arr
            return n.categoryId === catId;
        });
        console.log(newItems);
        let htmlData = "";
        for (let i = 0; i < newItems.length; i++) {
            htmlData = htmlData + ClsItems.Template1(newItems[i]);
        }
        $("#DivTemplate1").html(htmlData);
    }
}

ClsItems.LoadItems();
