(window["webpackJsonp"]=window["webpackJsonp"]||[]).push([["chunk-aa2892b2"],{1331:function(t,e,n){"use strict";Object.defineProperty(e,"__esModule",{value:!0}),e.default=void 0;var a=n("78ef"),c=(0,a.regex)("integer",/(^[0-9]*$)|(^-[0-9]+$)/);e.default=c},"1e64":function(t,e,n){"use strict";n.d(e,"a",(function(){return s}));var a=n("5530"),c=(n("d3b7"),n("99af"),n("a078")),r=n("57c3"),o=n("40d9"),i=n("bb4d"),u=n("df6c"),s={getAll:d,create:f,update:h,getById:m,remove:p,changeAvatar:b,createNote:g,createTag:y,removeNote:A,removeTag:O,loadTasks:l,loadAllObject:v};function l(t,e){return fetch("".concat(c["d"].apiUrl,"/contacts/").concat(e,"/tasks?").concat(Object(c["c"])(t)),r["a"].get()).then(o["a"]).then((function(t){return t}),(function(n){if("retry"==n)return Object(u["a"])("".concat(c["d"].apiUrl,"/contacts/").concat(e,"/tasks?").concat(Object(c["c"])(t)),r["a"].get(),2).then(o["a"])}))}function d(t){return fetch("".concat(c["d"].apiUrl,"/contacts?").concat(Object(c["c"])(t)),r["a"].get()).then(o["a"]).then((function(t){return t}),(function(e){if("retry"==e)return Object(u["a"])("".concat(c["d"].apiUrl,"/contacts?").concat(Object(c["c"])(t)),r["a"].get(),2).then(o["a"])}))}function f(t){return fetch("".concat(c["d"].apiUrl,"/contacts"),r["a"].post(t)).then(o["a"])}function p(t){return fetch("".concat(c["d"].apiUrl,"/contacts/").concat(t),r["a"].delete()).then(o["a"])}function m(t){return fetch("".concat(c["d"].apiUrl,"/contacts/").concat(t),r["a"].get()).then(o["a"]).then((function(t){return t}),(function(e){if("retry"==e)return Object(u["a"])("".concat(c["d"].apiUrl,"/contacts/").concat(t),r["a"].get(),2).then(o["a"])}))}function v(){return fetch("".concat(c["d"].apiUrl,"/contacts/prepare"),r["a"].get()).then(o["a"]).then((function(t){return t}),(function(t){if("retry"==t)return Object(u["a"])("".concat(c["d"].apiUrl,"/contacts/prepare"),r["a"].get(),2).then(o["a"])}))}function h(t,e){return fetch("".concat(c["d"].apiUrl,"/contacts/").concat(e),r["a"].post(t)).then(o["a"])}function b(t,e){return fetch("".concat(c["d"].apiUrl,"/contacts/").concat(e,"/change_avatar"),Object(a["a"])(Object(a["a"])({method:"POST"},j()),{},{body:t})).then(o["a"])}function g(t,e){return fetch("".concat(c["d"].apiUrl,"/contacts/").concat(e,"/notes"),Object(a["a"])(Object(a["a"])({method:"POST"},j()),{},{body:t})).then(o["a"])}function y(t,e){return fetch("".concat(c["d"].apiUrl,"/contacts/").concat(e,"/tags"),r["a"].post(t)).then(o["a"])}function A(t,e){return fetch("".concat(c["d"].apiUrl,"/contacts/").concat(t,"/notes/").concat(e),r["a"].delete()).then(o["a"])}function O(t,e){return fetch("".concat(c["d"].apiUrl,"/contacts/").concat(t,"/tags/").concat(e),r["a"].delete()).then(o["a"])}function j(){var t=i["a"].currentUserValue||{},e=t.jwt?{Authorization:t.jwt}:{};return{headers:Object(a["a"])({},e)}}},"2a12":function(t,e,n){"use strict";Object.defineProperty(e,"__esModule",{value:!0}),e.default=void 0;var a=n("78ef"),c=function(t){return(0,a.withParams)({type:"maxLength",max:t},(function(e){return!(0,a.req)(e)||(0,a.len)(e)<=t}))};e.default=c},3360:function(t,e,n){"use strict";Object.defineProperty(e,"__esModule",{value:!0}),e.default=void 0;var a=n("78ef"),c=function(){for(var t=arguments.length,e=new Array(t),n=0;n<t;n++)e[n]=arguments[n];return(0,a.withParams)({type:"and"},(function(){for(var t=this,n=arguments.length,a=new Array(n),c=0;c<n;c++)a[c]=arguments[c];return e.length>0&&e.reduce((function(e,n){return e&&n.apply(t,a)}),!0)}))};e.default=c},"3a54":function(t,e,n){"use strict";Object.defineProperty(e,"__esModule",{value:!0}),e.default=void 0;var a=n("78ef"),c=(0,a.regex)("alphaNum",/^[a-zA-Z0-9]*$/);e.default=c},"406d":function(t,e){t.exports="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAACAAAAAgCAYAAABzenr0AAAACXBIWXMAAAsTAAALEwEAmpwYAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAMOSURBVHgB1VbPTxNREJ5ta6EItipckEg1nEw0ImcTT96MXvgj1APcbPQAN9G/wcRgNDXcOZlALFzVRE6eFE4E0tJibWvZ3XFmO6+8bndLd7sXv+Trtm/fm+97M+9HAQIAETPEJeIm8See4kjaFojTEDUoaFYE+sXbyIzIrI4wOHjMAgwCSfegWIIwoIGLGBEsy1oMJF6v169huLT7gWNl+zaArUUUNTb7EpfZh8b3ookffvz1fFculy+69WLuhng8/ghCYqdkwYP1GjzdasBO0ep6n06nF9yabgOxRCLxEAYQrzQRns0m4ebleFcfSsI9fvjFMIhxDLH4OO3T744x86aCL7/UffvZtv2LNXoZSPQS2v1thRZXmJubOwcepTek0dfA4881vPXxuMNEUHEGaXQYiHmY2PVKz9VRA/aqSHX+Q0+7q+a5O8PQB8r8MT8/b4he60PPwMnJySdaiHe9Rq98bcCrb03HTKUJQcWBTsQCxb7PX4XorgU2Go0tvwAsxIKciaDijFqttu73jo3w6hxaXV29clYdud791lxHoVC4QRpJ0eqYvKEMEM9TGQoYMeiEfU+xR4nDomUAdG8HXqW4sbHxhMZUICJwLJr9isS3l5eXPQ8jdsVbJDU+Pj5WKpVyGBE4FsUdI46IRjsDOpyDCFop4lSl6fJYwQHBMTgWnKafNWJeGVDrIClO2XHm4ODgOR2hZQwI2nIVHksx+Aa8IDGTfuLKgHMWZLNZlQUeeGltbe12tVrN9ytOW3k7n8/P8liJ0Z491d/XgDsLKVUKCeQY2d/ff9FsNrd5hkrQNM09bisWi69zudx16c8zz0iMlDZ7wy3oRkxjYmJiInZ4eKjOCUPrgx4xUGjLb1NoC/kiAcMwsJcBt4k4nB4cMe29F2zt6QgKbY1dQl5AVwBzcnJSn43pIaC/U+wpfhbUjeWUAlo15IU0Ihx1kdtS0of7qgXXvvnCQpnQjSgOzczM8PE9pLVxH71kPcV7bgmBKod66qWwpqam2mWCzpTr46IDOn9qOv5HuPl/4R/DUdHG8zwpzQAAAABJRU5ErkJggg=="},"45b8":function(t,e,n){"use strict";Object.defineProperty(e,"__esModule",{value:!0}),e.default=void 0;var a=n("78ef"),c=(0,a.regex)("numeric",/^[0-9]*$/);e.default=c},"46bc":function(t,e,n){"use strict";Object.defineProperty(e,"__esModule",{value:!0}),e.default=void 0;var a=n("78ef"),c=function(t){return(0,a.withParams)({type:"maxValue",max:t},(function(e){return!(0,a.req)(e)||(!/\s/.test(e)||e instanceof Date)&&+e<=+t}))};e.default=c},"498a":function(t,e,n){"use strict";var a=n("23e7"),c=n("58a8").trim,r=n("c8d2");a({target:"String",proto:!0,forced:r("trim")},{trim:function(){return c(this)}})},"5d75":function(t,e,n){"use strict";Object.defineProperty(e,"__esModule",{value:!0}),e.default=void 0;var a=n("78ef"),c=/^(?:[A-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[A-z0-9!#$%&'*+/=?^_`{|}~-]+)*|"(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|[\x01-\x09\x0b\x0c\x0e-\x7f])*")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9]{2,}(?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])$/,r=(0,a.regex)("email",c);e.default=r},"5db3":function(t,e,n){"use strict";Object.defineProperty(e,"__esModule",{value:!0}),e.default=void 0;var a=n("78ef"),c=function(t){return(0,a.withParams)({type:"minLength",min:t},(function(e){return!(0,a.req)(e)||(0,a.len)(e)>=t}))};e.default=c},6235:function(t,e,n){"use strict";Object.defineProperty(e,"__esModule",{value:!0}),e.default=void 0;var a=n("78ef"),c=(0,a.regex)("alpha",/^[a-zA-Z]*$/);e.default=c},6417:function(t,e,n){"use strict";Object.defineProperty(e,"__esModule",{value:!0}),e.default=void 0;var a=n("78ef"),c=function(t){return(0,a.withParams)({type:"not"},(function(e,n){return!(0,a.req)(e)||!t.call(this,e,n)}))};e.default=c},7082:function(t,e,n){"use strict";n.d(e,"a",(function(){return s}));var a=n("5530"),c=(n("d3b7"),n("99af"),n("a078")),r=n("57c3"),o=n("40d9"),i=n("bb4d"),u=n("df6c"),s={getAll:f,create:l,update:d,getById:p,changePassword:m,remove:v,getAllTasks:h,getAllLeads:b,getAllAccounts:g,getAllContacts:y,changeAvatar:A};function l(t){return fetch("".concat(c["d"].apiUrl,"/users"),r["a"].post(t)).then(o["a"])}function d(t,e){return fetch("".concat(c["d"].apiUrl,"/users/").concat(e),r["a"].post(t)).then(o["a"])}function f(t){return fetch("".concat(c["d"].apiUrl,"/users?").concat(Object(c["c"])(t)),r["a"].get()).then(o["a"]).then((function(t){return t}),(function(e){if("retry"==e)return Object(u["a"])("".concat(c["d"].apiUrl,"/users?").concat(Object(c["c"])(t)),r["a"].get(),2).then(o["a"])}))}function p(t){return fetch("".concat(c["d"].apiUrl,"/users/").concat(t),r["a"].get()).then(o["a"]).then((function(t){return t}),(function(e){if("retry"==e)return Object(u["a"])("".concat(c["d"].apiUrl,"/accounts/").concat(t),r["a"].get(),2).then(o["a"])}))}function m(t,e,n){return fetch("".concat(c["d"].apiUrl,"/users/").concat(t,"/change_password"),r["a"].post({oldPassword:e,newPassword:n})).then(o["a"])}function v(t){return fetch("".concat(c["d"].apiUrl,"/users/").concat(t),r["a"].delete()).then(o["a"])}function h(t,e){return fetch("".concat(c["d"].apiUrl,"/users/").concat(t,"/tasks?").concat(Object(c["c"])(e)),r["a"].get()).then(o["a"]).then((function(t){return t}),(function(n){if("retry"==n)return Object(u["a"])("".concat(c["d"].apiUrl,"/users/").concat(t,"/tasks?").concat(Object(c["c"])(e)),r["a"].get(),2).then(o["a"])}))}function b(t,e){return fetch("".concat(c["d"].apiUrl,"/users/").concat(t,"/leads?").concat(Object(c["c"])(e)),r["a"].get()).then(o["a"]).then((function(t){return t}),(function(n){if("retry"==n)return Object(u["a"])("".concat(c["d"].apiUrl,"/users/").concat(t,"/leads?").concat(Object(c["c"])(e)),r["a"].get(),2).then(o["a"])}))}function g(t,e){return fetch("".concat(c["d"].apiUrl,"/users/").concat(t,"/accounts?").concat(Object(c["c"])(e)),r["a"].get()).then(o["a"]).then((function(t){return t}),(function(n){if("retry"==n)return Object(u["a"])("".concat(c["d"].apiUrl,"/users/").concat(t,"/accounts?").concat(Object(c["c"])(e)),r["a"].get(),2).then(o["a"])}))}function y(t,e){return fetch("".concat(c["d"].apiUrl,"/users/").concat(t,"/contacts?").concat(Object(c["c"])(e)),r["a"].get()).then(o["a"]).then((function(t){return t}),(function(n){if("retry"==n)return Object(u["a"])("".concat(c["d"].apiUrl,"/users/").concat(t,"/contacts?").concat(Object(c["c"])(e)),r["a"].get(),2).then(o["a"])}))}function A(t,e){return fetch("".concat(c["d"].apiUrl,"/users/").concat(e,"/change_avatar"),Object(a["a"])(Object(a["a"])({method:"POST"},O()),{},{body:t})).then(o["a"])}function O(){var t=i["a"].currentUserValue||{},e=t.jwt?{Authorization:t.jwt}:{};return{headers:Object(a["a"])({},e)}}},"772d4":function(t,e,n){"use strict";Object.defineProperty(e,"__esModule",{value:!0}),e.default=void 0;var a=n("78ef"),c=/^(?:(?:https?|ftp):\/\/)(?:\S+(?::\S*)?@)?(?:(?!(?:10|127)(?:\.\d{1,3}){3})(?!(?:169\.254|192\.168)(?:\.\d{1,3}){2})(?!172\.(?:1[6-9]|2\d|3[0-1])(?:\.\d{1,3}){2})(?:[1-9]\d?|1\d\d|2[01]\d|22[0-3])(?:\.(?:1?\d{1,2}|2[0-4]\d|25[0-5])){2}(?:\.(?:[1-9]\d?|1\d\d|2[0-4]\d|25[0-4]))|(?:(?:[a-z\u00a1-\uffff0-9]-*)*[a-z\u00a1-\uffff0-9]+)(?:\.(?:[a-z\u00a1-\uffff0-9]-*)*[a-z\u00a1-\uffff0-9]+)*(?:\.(?:[a-z\u00a1-\uffff]{2,})))(?::\d{2,5})?(?:[/?#]\S*)?$/i,r=(0,a.regex)("url",c);e.default=r},"78ef":function(t,e,n){"use strict";Object.defineProperty(e,"__esModule",{value:!0}),Object.defineProperty(e,"withParams",{enumerable:!0,get:function(){return a.default}}),e.regex=e.ref=e.len=e.req=void 0;var a=c(n("8750"));function c(t){return t&&t.__esModule?t:{default:t}}function r(t){return r="function"===typeof Symbol&&"symbol"===typeof Symbol.iterator?function(t){return typeof t}:function(t){return t&&"function"===typeof Symbol&&t.constructor===Symbol&&t!==Symbol.prototype?"symbol":typeof t},r(t)}var o=function(t){if(Array.isArray(t))return!!t.length;if(void 0===t||null===t)return!1;if(!1===t)return!0;if(t instanceof Date)return!isNaN(t.getTime());if("object"===r(t)){for(var e in t)return!0;return!1}return!!String(t).length};e.req=o;var i=function(t){return Array.isArray(t)?t.length:"object"===r(t)?Object.keys(t).length:String(t).length};e.len=i;var u=function(t,e,n){return"function"===typeof t?t.call(e,n):n[t]};e.ref=u;var s=function(t,e){return(0,a.default)({type:t},(function(t){return!o(t)||e.test(t)}))};e.regex=s},8750:function(t,e,n){"use strict";Object.defineProperty(e,"__esModule",{value:!0}),e.default=void 0;var a="web"===Object({NODE_ENV:"production",BASE_URL:"/"}).BUILD?n("cb69").withParams:n("0234").withParams,c=a;e.default=c},"91d3":function(t,e,n){"use strict";Object.defineProperty(e,"__esModule",{value:!0}),e.default=void 0;var a=n("78ef"),c=function(){var t=arguments.length>0&&void 0!==arguments[0]?arguments[0]:":";return(0,a.withParams)({type:"macAddress"},(function(e){if(!(0,a.req)(e))return!0;if("string"!==typeof e)return!1;var n="string"===typeof t&&""!==t?e.split(t):12===e.length||16===e.length?e.match(/.{2}/g):null;return null!==n&&(6===n.length||8===n.length)&&n.every(r)}))};e.default=c;var r=function(t){return t.toLowerCase().match(/^[0-9a-f]{2}$/)}},aa82:function(t,e,n){"use strict";Object.defineProperty(e,"__esModule",{value:!0}),e.default=void 0;var a=n("78ef"),c=function(t){return(0,a.withParams)({type:"requiredIf",prop:t},(function(e,n){return!(0,a.ref)(t,this,n)||(0,a.req)(e)}))};e.default=c},b5ae:function(t,e,n){"use strict";Object.defineProperty(e,"__esModule",{value:!0}),Object.defineProperty(e,"alpha",{enumerable:!0,get:function(){return a.default}}),Object.defineProperty(e,"alphaNum",{enumerable:!0,get:function(){return c.default}}),Object.defineProperty(e,"numeric",{enumerable:!0,get:function(){return r.default}}),Object.defineProperty(e,"between",{enumerable:!0,get:function(){return o.default}}),Object.defineProperty(e,"email",{enumerable:!0,get:function(){return i.default}}),Object.defineProperty(e,"ipAddress",{enumerable:!0,get:function(){return u.default}}),Object.defineProperty(e,"macAddress",{enumerable:!0,get:function(){return s.default}}),Object.defineProperty(e,"maxLength",{enumerable:!0,get:function(){return l.default}}),Object.defineProperty(e,"minLength",{enumerable:!0,get:function(){return d.default}}),Object.defineProperty(e,"required",{enumerable:!0,get:function(){return f.default}}),Object.defineProperty(e,"requiredIf",{enumerable:!0,get:function(){return p.default}}),Object.defineProperty(e,"requiredUnless",{enumerable:!0,get:function(){return m.default}}),Object.defineProperty(e,"sameAs",{enumerable:!0,get:function(){return v.default}}),Object.defineProperty(e,"url",{enumerable:!0,get:function(){return h.default}}),Object.defineProperty(e,"or",{enumerable:!0,get:function(){return b.default}}),Object.defineProperty(e,"and",{enumerable:!0,get:function(){return g.default}}),Object.defineProperty(e,"not",{enumerable:!0,get:function(){return y.default}}),Object.defineProperty(e,"minValue",{enumerable:!0,get:function(){return A.default}}),Object.defineProperty(e,"maxValue",{enumerable:!0,get:function(){return O.default}}),Object.defineProperty(e,"integer",{enumerable:!0,get:function(){return j.default}}),Object.defineProperty(e,"decimal",{enumerable:!0,get:function(){return w.default}}),e.helpers=void 0;var a=x(n("6235")),c=x(n("3a54")),r=x(n("45b8")),o=x(n("ec11")),i=x(n("5d75")),u=x(n("c99d")),s=x(n("91d3")),l=x(n("2a12")),d=x(n("5db3")),f=x(n("d4f4")),p=x(n("aa82")),m=x(n("e652")),v=x(n("b6cb")),h=x(n("772d4")),b=x(n("d294")),g=x(n("3360")),y=x(n("6417")),A=x(n("eb66")),O=x(n("46bc")),j=x(n("1331")),w=x(n("c301")),P=_(n("78ef"));function _(t){if(t&&t.__esModule)return t;var e={};if(null!=t)for(var n in t)if(Object.prototype.hasOwnProperty.call(t,n)){var a=Object.defineProperty&&Object.getOwnPropertyDescriptor?Object.getOwnPropertyDescriptor(t,n):{};a.get||a.set?Object.defineProperty(e,n,a):e[n]=t[n]}return e.default=t,e}function x(t){return t&&t.__esModule?t:{default:t}}e.helpers=P},b6cb:function(t,e,n){"use strict";Object.defineProperty(e,"__esModule",{value:!0}),e.default=void 0;var a=n("78ef"),c=function(t){return(0,a.withParams)({type:"sameAs",eq:t},(function(e,n){return e===(0,a.ref)(t,this,n)}))};e.default=c},beb4:function(t,e,n){"use strict";n("d38c")},c301:function(t,e,n){"use strict";Object.defineProperty(e,"__esModule",{value:!0}),e.default=void 0;var a=n("78ef"),c=(0,a.regex)("decimal",/^[-]?\d*(\.\d+)?$/);e.default=c},c8d2:function(t,e,n){var a=n("d039"),c=n("5899"),r="​᠎";t.exports=function(t){return a((function(){return!!c[t]()||r[t]()!=r||c[t].name!==t}))}},c99d:function(t,e,n){"use strict";Object.defineProperty(e,"__esModule",{value:!0}),e.default=void 0;var a=n("78ef"),c=(0,a.withParams)({type:"ipAddress"},(function(t){if(!(0,a.req)(t))return!0;if("string"!==typeof t)return!1;var e=t.split(".");return 4===e.length&&e.every(r)}));e.default=c;var r=function(t){if(t.length>3||0===t.length)return!1;if("0"===t[0]&&"0"!==t)return!1;if(!t.match(/^\d+$/))return!1;var e=0|+t;return e>=0&&e<=255}},cb69:function(t,e,n){"use strict";(function(t){function n(t){return n="function"===typeof Symbol&&"symbol"===typeof Symbol.iterator?function(t){return typeof t}:function(t){return t&&"function"===typeof Symbol&&t.constructor===Symbol&&t!==Symbol.prototype?"symbol":typeof t},n(t)}Object.defineProperty(e,"__esModule",{value:!0}),e.withParams=void 0;var a="undefined"!==typeof window?window:"undefined"!==typeof t?t:{},c=function(t,e){return"object"===n(t)&&void 0!==e?e:t((function(){}))},r=a.vuelidate?a.vuelidate.withParams:c;e.withParams=r}).call(this,n("c8ba"))},d294:function(t,e,n){"use strict";Object.defineProperty(e,"__esModule",{value:!0}),e.default=void 0;var a=n("78ef"),c=function(){for(var t=arguments.length,e=new Array(t),n=0;n<t;n++)e[n]=arguments[n];return(0,a.withParams)({type:"or"},(function(){for(var t=this,n=arguments.length,a=new Array(n),c=0;c<n;c++)a[c]=arguments[c];return e.length>0&&e.reduce((function(e,n){return e||n.apply(t,a)}),!1)}))};e.default=c},d38c:function(t,e,n){},d4f4:function(t,e,n){"use strict";Object.defineProperty(e,"__esModule",{value:!0}),e.default=void 0;var a=n("78ef"),c=(0,a.withParams)({type:"required"},(function(t){return"string"===typeof t?(0,a.req)(t.trim()):(0,a.req)(t)}));e.default=c},dfbf:function(t,e,n){"use strict";n.d(e,"a",(function(){return s}));var a=n("5530"),c=(n("d3b7"),n("99af"),n("a078")),r=n("57c3"),o=n("40d9"),i=n("bb4d"),u=n("df6c"),s={getAll:l,create:d,update:h,getById:p,remove:f,changeAvatar:b,createNote:g,createTag:y,removeNote:A,removeTag:O,loadAllObject:v,loadContacts:m};function l(t){return fetch("".concat(c["d"].apiUrl,"/accounts?").concat(Object(c["c"])(t)),r["a"].get()).then(o["a"]).then((function(t){return t}),(function(e){if("retry"==e)return Object(u["a"])("".concat(c["d"].apiUrl,"/accounts?").concat(Object(c["c"])(t)),r["a"].get(),2).then(o["a"])}))}function d(t){return fetch("".concat(c["d"].apiUrl,"/accounts"),r["a"].post(t)).then(o["a"])}function f(t){return fetch("".concat(c["d"].apiUrl,"/accounts/").concat(t),r["a"].delete()).then(o["a"])}function p(t){return fetch("".concat(c["d"].apiUrl,"/accounts/").concat(t),r["a"].get()).then(o["a"]).then((function(t){return t}),(function(e){if("retry"==e)return Object(u["a"])("".concat(c["d"].apiUrl,"/accounts/").concat(t),r["a"].get(),2).then(o["a"])}))}function m(t,e){return fetch("".concat(c["d"].apiUrl,"/accounts/").concat(e,"/contacts?").concat(Object(c["c"])(t)),r["a"].get()).then(o["a"]).then((function(t){return t}),(function(n){if("retry"==n)return Object(u["a"])("".concat(c["d"].apiUrl,"/accounts/").concat(e,"/contacts?").concat(Object(c["c"])(t)),r["a"].get(),2).then(o["a"])}))}function v(){return fetch("".concat(c["d"].apiUrl,"/accounts/prepare"),r["a"].get()).then(o["a"]).then((function(t){return t}),(function(t){if("retry"==t)return Object(u["a"])("".concat(c["d"].apiUrl,"/accounts/prepare"),r["a"].get(),2).then(o["a"])}))}function h(t,e){return fetch("".concat(c["d"].apiUrl,"/accounts/").concat(e),r["a"].post(t)).then(o["a"])}function b(t,e){return fetch("".concat(c["d"].apiUrl,"/accounts/").concat(e,"/change_avatar"),Object(a["a"])(Object(a["a"])({method:"POST"},j()),{},{body:t})).then(o["a"])}function g(t,e){return fetch("".concat(c["d"].apiUrl,"/accounts/").concat(e,"/notes"),Object(a["a"])(Object(a["a"])({method:"POST"},j()),{},{body:t})).then(o["a"])}function y(t,e){return fetch("".concat(c["d"].apiUrl,"/accounts/").concat(e,"/tags"),r["a"].post(t)).then(o["a"])}function A(t,e){return fetch("".concat(c["d"].apiUrl,"/accounts/").concat(t,"/notes/").concat(e),r["a"].delete()).then(o["a"])}function O(t,e){return fetch("".concat(c["d"].apiUrl,"/accounts/").concat(t,"/tags/").concat(e),r["a"].delete()).then(o["a"])}function j(){var t=i["a"].currentUserValue||{},e=t.jwt?{Authorization:t.jwt}:{};return{headers:Object(a["a"])({},e)}}},e429:function(t,e){t.exports="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABgAAAAYCAYAAADgdz34AAAACXBIWXMAAAsTAAALEwEAmpwYAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAACpSURBVHgB7ZRvDYQwDMW7U8ApACunBAmcgzsHSEACEpCAhOEAHJQuaUjTbAnl36f9kpeFt7aPBDaAjAVErEkzHseHGXKmUwGelgrOsTjn3qkAhAuggG3uC24mB+SAJAvpw+t+EqdziHg11zexBktAz34nvJ/qaY8GeFLJfkEagyI9BdeaA0q1V2lP7c2WgAaMyO8h/ccvO9tvF2eSDzrgqwuMhBf8Q8bCChiTPhKJw70GAAAAAElFTkSuQmCC"},e652:function(t,e,n){"use strict";Object.defineProperty(e,"__esModule",{value:!0}),e.default=void 0;var a=n("78ef"),c=function(t){return(0,a.withParams)({type:"requiredUnless",prop:t},(function(e,n){return!!(0,a.ref)(t,this,n)||(0,a.req)(e)}))};e.default=c},eb66:function(t,e,n){"use strict";Object.defineProperty(e,"__esModule",{value:!0}),e.default=void 0;var a=n("78ef"),c=function(t){return(0,a.withParams)({type:"minValue",min:t},(function(e){return!(0,a.req)(e)||(!/\s/.test(e)||e instanceof Date)&&+e>=+t}))};e.default=c},ec11:function(t,e,n){"use strict";Object.defineProperty(e,"__esModule",{value:!0}),e.default=void 0;var a=n("78ef"),c=function(t,e){return(0,a.withParams)({type:"between",min:t,max:e},(function(n){return!(0,a.req)(n)||(!/\s/.test(n)||n instanceof Date)&&+t<=+n&&+e>=+n}))};e.default=c},ec8d:function(t,e,n){"use strict";n.r(e);var a=function(){var t=this,e=t.$createElement,a=t._self._c||e;return a("div",{},[a("div",{staticClass:"px-5 pt-3 m-0 background-main"},[a("div",{staticClass:"row "},[a("div",{staticClass:"col-sm-8"}),a("div",{staticClass:"col-sm-4 d-flex justify-content-end"},[a("router-link",{attrs:{to:{name:"ContactList"}}},[a("VButton",{attrs:{data:t.btnCancel}})],1),a("span",{staticClass:"ml-5",on:{click:function(e){return t.save()}}},[a("VButton",{attrs:{data:t.btnSave}})],1)],1)]),a("div",{staticClass:"mt-3"},[a("VLoading",{attrs:{loading:t.loading}}),a("input",{staticStyle:{display:"none"},attrs:{id:"avatarId",type:"file"},on:{change:function(e){return t.fileChange(e)}}}),a("div",{staticClass:"row mt-3"},[t.contact.id?a("div",{staticClass:"basic-edit"},[t._m(0),a("div",{staticClass:"row"},[a("div",{staticClass:"user-info-avatar"},[a("div",{staticClass:"user-info-avatar-image"},[t.contact.avatar?a("img",{staticClass:"w-100",attrs:{src:t.contact.avatar,alt:""}}):a("img",{staticClass:"w-100",attrs:{src:n("8ae4"),alt:""}})]),a("div",{staticClass:"image-select-background"}),t._m(1)])])]):t._e(),a("div",{staticClass:"basic-edit"},[t._m(2),a("div",{staticClass:"row"},[a("div",{staticClass:"col-sm-5"},[a("table",{staticClass:"ml-4"},[a("tbody",[a("tr",[a("td",[t._v("Contact Owner")]),a("td",{staticStyle:{width:"80%"}},[a("vc-select",{attrs:{id:"cOwner",label:"username",filterable:!1,options:t.owners},on:{search:t.onSearch},model:{value:t.contact.owner,callback:function(e){t.$set(t.contact,"owner",e)},expression:"contact.owner"}})],1)]),a("tr",{class:{"form-group--error":t.$v.contact.name.$error}},[a("td",{staticClass:"required"},[t._v("Name")]),a("td",[a("input",{directives:[{name:"model",rawName:"v-model",value:t.contact.name,expression:"contact.name"}],staticClass:"form-control ",attrs:{type:"text"},domProps:{value:t.contact.name},on:{input:function(e){e.target.composing||t.$set(t.contact,"name",e.target.value)}}})])]),a("tr",{class:{"form-group--error":t.$v.contact.email.$error}},[a("td",{staticClass:"required"},[t._v("Email")]),a("td",[a("input",{directives:[{name:"model",rawName:"v-model.trim",value:t.$v.contact.email.$model,expression:"$v.contact.email.$model",modifiers:{trim:!0}}],staticClass:"form-control",attrs:{type:"text"},domProps:{value:t.$v.contact.email.$model},on:{input:function(e){e.target.composing||t.$set(t.$v.contact.email,"$model",e.target.value.trim())},blur:function(e){return t.$forceUpdate()}}}),t.$v.contact.email.isValidEmail?t._e():a("small",{staticClass:"error text-danger"},[t._v("Email is not correct.")])])]),a("tr",[a("td",[t._v("Phone")]),a("td",[a("input",{directives:[{name:"model",rawName:"v-model",value:t.contact.phone,expression:"contact.phone"}],staticClass:"form-control ",attrs:{type:"text"},domProps:{value:t.contact.phone},on:{input:function(e){e.target.composing||t.$set(t.contact,"phone",e.target.value)}}})])]),a("tr",[a("td",[t._v("Mobile")]),a("td",[a("input",{directives:[{name:"model",rawName:"v-model",value:t.contact.mobile,expression:"contact.mobile"}],staticClass:"form-control ",attrs:{type:"text"},domProps:{value:t.contact.mobile},on:{input:function(e){e.target.composing||t.$set(t.contact,"mobile",e.target.value)}}})])]),a("tr",[a("td",[t._v("Department Name")]),a("td",[a("input",{directives:[{name:"model",rawName:"v-model",value:t.contact.departmentName,expression:"contact.departmentName"}],staticClass:"form-control ",attrs:{type:"text"},domProps:{value:t.contact.departmentName},on:{input:function(e){e.target.composing||t.$set(t.contact,"departmentName",e.target.value)}}})])]),a("tr",[a("td",[t._v("Birthday")]),a("td",[a("input",{directives:[{name:"model",rawName:"v-model",value:t.contact.birthday,expression:"contact.birthday"}],staticClass:"form-control ",attrs:{type:"date"},domProps:{value:t.contact.birthday},on:{input:function(e){e.target.composing||t.$set(t.contact,"birthday",e.target.value)}}})])]),a("tr",[t._m(3),a("td",{staticStyle:{width:"80%"}},[a("vc-select",{attrs:{id:"Collaborator",label:"username",filterable:!1,options:t.owners},on:{search:t.onSearch},model:{value:t.contact.collaborator,callback:function(e){t.$set(t.contact,"collaborator",e)},expression:"contact.collaborator"}})],1)])])])]),a("div",{staticClass:"col-sm-1"}),a("div",{staticClass:"col-sm-5"},[a("table",[a("tbody",[a("tr",[a("td",[t._v("No Email")]),a("td",[a("input",{directives:[{name:"model",rawName:"v-model",value:t.contact.noEmail,expression:"contact.noEmail"}],staticClass:"form-control",attrs:{type:"checkbox"},domProps:{checked:Array.isArray(t.contact.noEmail)?t._i(t.contact.noEmail,null)>-1:t.contact.noEmail},on:{change:function(e){var n=t.contact.noEmail,a=e.target,c=!!a.checked;if(Array.isArray(n)){var r=null,o=t._i(n,r);a.checked?o<0&&t.$set(t.contact,"noEmail",n.concat([r])):o>-1&&t.$set(t.contact,"noEmail",n.slice(0,o).concat(n.slice(o+1)))}else t.$set(t.contact,"noEmail",c)}}})])]),a("tr",[a("td",[t._v("No Call")]),a("td",[a("input",{directives:[{name:"model",rawName:"v-model",value:t.contact.noCall,expression:"contact.noCall"}],staticClass:"form-control ",attrs:{type:"checkbox"},domProps:{checked:Array.isArray(t.contact.noCall)?t._i(t.contact.noCall,null)>-1:t.contact.noCall},on:{change:function(e){var n=t.contact.noCall,a=e.target,c=!!a.checked;if(Array.isArray(n)){var r=null,o=t._i(n,r);a.checked?o<0&&t.$set(t.contact,"noCall",n.concat([r])):o>-1&&t.$set(t.contact,"noCall",n.slice(0,o).concat(n.slice(o+1)))}else t.$set(t.contact,"noCall",c)}}})])]),a("tr",[a("td",[t._v("Skype")]),a("td",[a("input",{directives:[{name:"model",rawName:"v-model",value:t.contact.skype,expression:"contact.skype"}],staticClass:"form-control ",attrs:{type:"text"},domProps:{value:t.contact.skype},on:{input:function(e){e.target.composing||t.$set(t.contact,"skype",e.target.value)}}})])]),a("tr",[a("td",[t._v("Assistant Name")]),a("td",[a("input",{directives:[{name:"model",rawName:"v-model",value:t.contact.assistantName,expression:"contact.assistantName"}],staticClass:"form-control ",attrs:{type:"text"},domProps:{value:t.contact.assistantName},on:{input:function(e){e.target.composing||t.$set(t.contact,"assistantName",e.target.value)}}})])]),a("tr",[a("td",[t._v("Assistant Phone")]),a("td",[a("input",{directives:[{name:"model",rawName:"v-model",value:t.contact.assistantPhone,expression:"contact.assistantPhone"}],staticClass:"form-control ",attrs:{type:"text"},domProps:{value:t.contact.assistantPhone},on:{input:function(e){e.target.composing||t.$set(t.contact,"assistantPhone",e.target.value)}}})])]),a("tr",[t._m(4),a("td",[t.priorities?a("select",{directives:[{name:"model",rawName:"v-model.trim",value:t.contact.priority,expression:"contact.priority",modifiers:{trim:!0}}],staticClass:"form-control py-0 w-100",attrs:{id:"Priority"},on:{change:function(e){var n=Array.prototype.filter.call(e.target.options,(function(t){return t.selected})).map((function(t){var e="_value"in t?t._value:t.value;return e}));t.$set(t.contact,"priority",e.target.multiple?n:n[0])}}},t._l(t.priorities,(function(e,n){return a("option",{key:n,domProps:{value:e.id}},[t._v(t._s(e.name))])})),0):t._e()])]),a("tr",[t._m(5),a("td",{staticStyle:{width:"80%"}},[a("vc-select",{attrs:{id:"Account",label:"name",filterable:!1,options:t.accounts},on:{search:t.onSearchAccount},model:{value:t.contact.account,callback:function(e){t.$set(t.contact,"account",e)},expression:"contact.account"}})],1)])])])])])]),a("div",{staticClass:"basic-edit"},[t._m(6),a("div",{staticClass:"row"},[a("div",{staticClass:"col-sm-12"},[a("table",{staticClass:"ml-4"},[a("tbody",[a("tr",{staticClass:"row"},[a("td",{staticClass:"col-sm-2"},[t._v("Country")]),a("td",{staticClass:"col-sm-3"},[a("input",{directives:[{name:"model",rawName:"v-model",value:t.contact.country,expression:"contact.country"}],staticClass:"form-control ",attrs:{type:"text"},domProps:{value:t.contact.country},on:{input:function(e){e.target.composing||t.$set(t.contact,"country",e.target.value)}}})])]),a("tr",{staticClass:"row"},[a("td",{staticClass:"col-sm-2"},[t._v("City")]),a("td",{staticClass:"col-sm-3"},[a("input",{directives:[{name:"model",rawName:"v-model",value:t.contact.city,expression:"contact.city"}],staticClass:"form-control ",attrs:{type:"text"},domProps:{value:t.contact.city},on:{input:function(e){e.target.composing||t.$set(t.contact,"city",e.target.value)}}})])]),a("tr",{staticClass:"row"},[a("td",{staticClass:"col-sm-2"},[t._v("Address Detail")]),a("td",{staticClass:"col-sm-9"},[a("input",{directives:[{name:"model",rawName:"v-model",value:t.contact.addressDetail,expression:"contact.addressDetail"}],staticClass:"form-control ",attrs:{type:"text"},domProps:{value:t.contact.addressDetail},on:{input:function(e){e.target.composing||t.$set(t.contact,"addressDetail",e.target.value)}}})])])])])])])])])],1)])])},c=[function(){var t=this,e=t.$createElement,n=t._self._c||e;return n("h4",{staticClass:"ml-2"},[n("strong",[t._v("User Avatar")])])},function(){var t=this,e=t.$createElement,a=t._self._c||e;return a("div",[a("label",{attrs:{for:"avatarId"}},[a("img",{staticClass:"image-select",attrs:{src:n("e429"),alt:""}})]),a("img",{staticClass:"image-done",attrs:{src:n("406d"),alt:""}})])},function(){var t=this,e=t.$createElement,n=t._self._c||e;return n("h4",{staticClass:"ml-2"},[n("strong",[t._v("Contact Information")])])},function(){var t=this,e=t.$createElement,n=t._self._c||e;return n("td",[n("label",{attrs:{for:"Collaborator"}},[t._v("Collaborator")])])},function(){var t=this,e=t.$createElement,n=t._self._c||e;return n("td",[n("label",{attrs:{for:"Priority"}},[t._v("Priority")])])},function(){var t=this,e=t.$createElement,n=t._self._c||e;return n("td",[n("label",{attrs:{for:"Account"}},[t._v("Account")])])},function(){var t=this,e=t.$createElement,n=t._self._c||e;return n("h4",{staticClass:"ml-2"},[n("strong",[t._v("Address Information")])])}],r=n("1da1"),o=(n("96cf"),n("d3b7"),n("2b3d"),n("3ca3"),n("ddb0"),n("498a"),n("a9e3"),n("a179")),i=n("1e64"),u=n("7082"),s=n("b5ae"),l=n("a078"),d=n("dfbf"),f=n("e118"),p={name:"LeadUpdate",components:{VLoading:f["a"],VButton:o["a"]},validations:{contact:{name:{required:s["required"]},email:{required:s["required"],isValidEmail:function(t){return""===t||this.validateEmail(t)}}}},methods:{save:function(){var t=this;return Object(r["a"])(regeneratorRuntime.mark((function e(){return regeneratorRuntime.wrap((function(e){while(1)switch(e.prev=e.next){case 0:if(t.$v.$touch(),!t.$v.$invalid){e.next=4;break}return alert("loi"),e.abrupt("return");case 4:if(t.loading=!0,t.contact.owner=t.contact.owner?t.contact.owner.id:null,t.contact.collaborator=t.contact.collaborator?t.contact.collaborator.id:null,t.contact.account=t.contact.account?t.contact.account.id:null,!t.contact.id){e.next=15;break}if(!t.files){e.next=12;break}return e.next=12,t.upload();case 12:i["a"].update(t.contact,t.contact.id).then((function(e){e&&(alert(e.message),t.$router.push("/contacts/detail?id="+t.contact.id))})).catch((function(t){return alert(t)})).finally((function(){t.loading=!1})),e.next=16;break;case 15:i["a"].create(t.contact).then((function(e){e&&(alert(e.message),t.accountId?t.$router.push("/accounts/detail?id="+t.accountId):t.$router.push("/contacts"))})).catch((function(t){return alert(t)})).finally((function(){t.loading=!1}));case 16:case"end":return e.stop()}}),e)})))()},loadContact:function(){var t=this;this.loading=!0,i["a"].getById(this.contact.id).then((function(e){e&&e.data?(t.contact=e.data,t.contact.birthday=Object(l["e"])(t.contact.birthday,"yyyy-MM-dd"),t.contact.priority=Object(l["f"])(e.data.priorities,"selected","id")):(alert("Không có dữ liệu"),t.$router.push("/"))})).finally((function(){t.loading=!1}))},loadSelectList:function(){var t=this;this.loading=!0,i["a"].loadAllObject().then((function(e){e&&e.data&&"success"===e.status&&(t.priorities=e.data.priority)})).finally((function(){t.loading=!1}))},fileChange:function(t){this.files=t.target.files[0],this.contact.avatar=URL.createObjectURL(this.files)},upload:function(){var t=new FormData;t.append("file",this.files),i["a"].changeAvatar(t,this.contact.id).then((function(t){console.log(t)})).catch((function(t){return alert(t)}))},onSearch:function(t){var e=this,n={currentPage:1,pageSize:10};t&&(n["query"]=t),u["a"].getAll(n).then((function(t){t&&(e.owners=t.data.users)}))},onSearchAccount:function(t){var e=this,n={currentPage:1,pageSize:10};t&&t.trim()&&(n["query"]=t),d["a"].getAll(n).then((function(t){t&&t.data&&(e.accounts=t.data.accounts)}))},validateEmail:function(t){return/^\w+([.-]?\w+)*@\w+([.-]?\w+)*(\.\w{2,3})+$/.test(t)},getAccountById:function(){var t=this;d["a"].getById(this.accountId).then((function(e){e&&e.data&&(t.contact.account=e.data)}))}},created:function(){if(this.$route.path.indexOf("contacts/update")>-1){if(!this.$route.query.id)return void this.$router.push("/");this.contact.id=this.$route.query.id,this.loadContact()}else this.$route.query.accountId&&(this.accountId=Number(this.$route.query.accountId),this.getAccountById());this.loadSelectList(),this.onSearch(null),this.onSearchAccount(null)},data:function(){return{contact:{owner:null,account:null,collaborator:null,name:null,email:null,phone:null,mobile:null,departmentName:null,birthday:null,priority:0,noEmail:!0,noCall:!0,skype:null,assistantName:null,assistantPhone:null,country:null,city:null,addressDetail:null},accountId:null,campaignId:null,owners:[],priorities:[],accounts:[],loading:!1,files:null,btnCancel:{btnClass:"btn-white px-3",icon:"fa-times",text:"Cancel"},btnSave:{btnClass:"btn-red px-4",icon:"fa-floppy-o",text:"Save"}}}},m=p,v=(n("beb4"),n("2877")),h=Object(v["a"])(m,a,c,!1,null,"5ca2eb0f",null);e["default"]=h.exports}}]);
//# sourceMappingURL=chunk-aa2892b2.49daa1de.js.map