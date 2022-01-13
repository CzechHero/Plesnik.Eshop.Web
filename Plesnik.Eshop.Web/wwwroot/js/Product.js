function Buy(productId, urlAction, outElementId, locale) {
    $.ajax({
        type: "POST",
        url: urlAction,
        data: { productId: productId },
        dataType: "text",
        success: function (totalPrice) {
            ChangeTotalPriceInformation(outElementId, locale, totalPrice);
        },
        error: function (req, status, error) {
            $(outElementId).text('error during buying!');
        }
    });
}

function ChangeTotalPriceInformation(outElementId, locale, totalPrice) {
    $(outElementId).text(parseFloat(totalPrice).toLocaleString(locale,
        {
            style: "currency",
            currency: "CZK",
            minimumFractionDigits: 2,
            maximumFractionDigits: 2
        }));
}


$(document).ready(function () {
    var fullUrl = document.URL;
    console.log(fullUrl);
    var urlArray = fullUrl.split('/');
    var lastSegment = urlArray[urlArray.length - 1];

    $("#relatedSearch").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: '/api/product/related',
                //headers: { "RequestVerificationToken": $('input[name="__RequestVerificationToken"]').val() },
                data: {
                    "productId": lastSegment,
                    "query": request.term
                },
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    response($.map(data, function (item, key) {
                        console.log(item);
                        console.log(key);
                        return item;
                    }))
                },
                error: function (xhr, textStatus, error) {
                    alert(xhr.statusText);
                },
                failure: function (response) {
                    alert("Failure " + response.responseText);
                }
            });
        },
        select: function (e, ui) {
            // Set autocomplete element to display the label
            this.value = ui.item.label;

            // Store Id Value
            $("#relatedSearchId").val(ui.item.value.id);

            // Display data in modal
            $("#relatedSearchImg").attr('src', ui.item.value.imageSource)
            $("#relatedSearchTitle").text(ui.item.value.name);
            $("#relatedSearchSubtitle").text('P-ID: ' + ui.item.value.id);
            $("#relatedSearchDesc").text(ui.item.value.description);
            $("#relatedSearchPrice").text(ui.item.value.price + ' CZK');
            
            // Collapse modal
            $("#collapseRelated").collapse('show');

            // Prevent default behaviour
            return false;
        },
        minLength: 3
    });

    $("#relatedSearch").autocomplete("option", "appendTo", "#eventInsForm");
});

function clearSearchData() {
    $('#relatedSearch').val('');
    $("#collapseRelated").collapse('hide');
}

function Suggest(productId, urlAction) {
    console.log("productId: " + productId);
    console.log("urlAction: " + urlAction);
    $.ajax({
        type: "POST",
        url: urlAction,
        data: {
            productId: productId,
            relatedId: $("#relatedSearchId").val()
        },
        dataType: "text",
        success: function () {
            console.log("ok");
            // Clear
            clearSearchData();

            $('#suggestModal').modal('hide');
        },
        error: function (req, status, error) {
            console.log("fail");
        }
    });
}

function AddRelated(productId, urlAction) {
    console.log("productId: " + productId);
    console.log("urlAction: " + urlAction);
    $.ajax({
        type: "POST",
        url: urlAction,
        data: {
            productId: productId,
            relatedId: $("#relatedSearchId").val()
        },
        dataType: "text",
        success: function () {
            console.log("ok");
            // Clear
            clearSearchData();

            $('#suggestModal').modal('hide');
            location.reload();
        },
        error: function (req, status, error) {
            console.log("fail");
        }
    });
}