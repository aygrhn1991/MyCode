angular.module('Pages', []).directive('pages', function ($http) {
    return {
        restrict: 'E',
        replace: true,
        scope: false,
        template: '<nav>\
            <ul class="pagination" style="cursor:pointer;">\
                <li ng-click="_firstpage()"><a>首页</a></li>\
                <li ng-click="_prepage()"><a><span>&laquo;</span></a></li>\
                <li ng-repeat="page in _pages" ng-class="{\'active\':page==pageconfig.currentpage}" ng-click="_loadbypage(page)"><a>{{page+1}}</a></li>\
                <li ng-click="_nextpage()"><a><span>&raquo;</span></a></li>\
                <li ng-click="_endpage()"><a>尾页<small>（共{{_totalpages}}页）</small></a></li>\
            </ul>\
        </nav>',
        link: function (scope, element, attrs) {
            scope.makepages = function (loadfunction, totalcounts, pagesize) {
                scope._load = loadfunction;
                scope._totalpages = Math.floor((totalcounts - 1) / pagesize) + 1;
                scope._pages = [];
                if (scope._totalpages <= 5) {
                    for (var i = 0; i < scope._totalpages; i++) {
                        scope._pages.push(i);
                    }
                } else {
                    if (scope.pageconfig.currentpage <= 2) {
                        for (var i = 0; i < 5; i++) {
                            scope._pages.push(i)
                        }
                    } else if (scope.pageconfig.currentpage + 2 >= scope._totalpages - 1) {
                        for (var i = 0; i < 5; i++) {
                            scope._pages.push(scope._totalpages - 5 + i)
                        }
                    } else {
                        for (var i = 0; i < 5; i++) {
                            scope._pages.push(scope.pageconfig.currentpage + i - 2)
                        }
                    }
                }
            };
            scope._loadbypage = function (e) {
                if (e == scope.pageconfig.currentpage) {
                    return;
                }
                scope.pageconfig.currentpage = e;
                scope._load();
            };
            scope._firstpage = function () {
                if (scope.pageconfig.currentpage == 0) {
                    return;
                }
                scope.pageconfig.currentpage = 0;
                scope._load();
            };
            scope._endpage = function () {
                if (scope.pageconfig.currentpage == scope._totalpages - 1) {
                    return;
                }
                scope.pageconfig.currentpage = scope._totalpages - 1;
                scope._load();
            };
            scope._prepage = function () {
                if (scope.pageconfig.currentpage <= 0) {
                    return;
                }
                scope.pageconfig.currentpage--;
                scope._load();
            };
            scope._nextpage = function () {
                if (scope.pageconfig.currentpage >= scope._totalpages - 1) {
                    return;
                }
                scope.pageconfig.currentpage++;
                scope._load();
            };
        },
    };
});
function showMessage(e) {
    if ($('#alert-message').length > 0) {
        $('#alert-message').remove();
    }
    var mask = '<div id="alert-message">\
            <div></div>\
            <div>\
                <div id="alert-message-tip">\
                    <div></div>\
                    <div></div>\
                    <div></div>\
                    <div></div>\
                    <div></div>\
                    <div></div>\
                    <div></div>\
                    <div></div>\
                </div>\
                <div>' + (typeof e == 'undefined' ? '处理中，请等待！' : e) + '</div>\
            </div>\
        </div>';
    $('body').append(mask);
};
function hideMessage() {
    $('#alert-message').remove();
};

function checkNumber(data, showalert, message) {
    var reg = new RegExp('^[0-9]+(.[0-9]+)?$');
    if (reg.test(data) == true) {
        return true;
    } else {
        if (showalert == true) {
            typeof message == 'undefined' ? alert('不是数字') : alert(message);
        }
        return false;
    }
};
function checkEmail(data, showalert, message) {
    var reg = new RegExp('^([a-zA-Z0-9_\.\-]+)@([a-zA-Z0-9_\.\-]+)\.([a-zA-Z]{2,4})$');
    if (reg.test(data) == true) {
        return true;
    } else {
        if (showalert == true) {
            typeof message == 'undefined' ? alert('不是Email格式') : alert(message);
        }
        return false;
    }
};
function checkNullOrUndefined(data, showalert, message) {
    if (data == null) {
        if (showalert == true) {
            typeof message == 'undefined' ? alert('该值为null') : alert(message);
        }
        return true;
    } else if (typeof data == 'undefined') {
        if (showalert == true) {
            typeof message == 'undefined' ? alert('该值为undefined') : alert(message);
        }
        return true;
    } else {
        return false;
    }
};
function checkEmpty(data, showalert, message) {
    if (data.length ==0||data=='') {
        if (showalert == true) {
            typeof message == 'undefined' ? alert('空值') : alert(message);
        }
        return true;
    } else {
        return false;
    }
};
var s = '123';
var r = checkEmpty(s, true);
console.log(r);
console.log('go');
