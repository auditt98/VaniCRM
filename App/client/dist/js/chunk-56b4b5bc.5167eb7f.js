(window["webpackJsonp"]=window["webpackJsonp"]||[]).push([["chunk-56b4b5bc"],{1031:function(t,e,a){},1206:function(t,e,a){"use strict";a("2eb1")},"2eb1":function(t,e,a){},7082:function(t,e,a){"use strict";a.d(e,"a",(function(){return i}));var n=a("5530"),r=(a("d3b7"),a("99af"),a("a078")),c=a("57c3"),s=a("40d9"),o=a("bb4d"),i={getAll:d,create:l,update:u,getById:f,changePassword:h,remove:p,getAllTasks:v,getAllLeads:g,getAllAccounts:b,getAllContacts:m,changeAvatar:y};function l(t){return fetch("".concat(r["d"].apiUrl,"/users"),c["a"].post(t)).then(s["a"])}function u(t,e){return fetch("".concat(r["d"].apiUrl,"/users/").concat(e),c["a"].post(t)).then(s["a"])}function d(t){return fetch("".concat(r["d"].apiUrl,"/users?").concat(Object(r["c"])(t)),c["a"].get()).then(s["a"])}function f(t){return fetch("".concat(r["d"].apiUrl,"/users/").concat(t),c["a"].get()).then(s["a"])}function h(t,e,a){return fetch("".concat(r["d"].apiUrl,"/users/").concat(t,"/change_password"),c["a"].post({oldPassword:e,newPassword:a})).then(s["a"])}function p(t){return fetch("".concat(r["d"].apiUrl,"/users/").concat(t),c["a"].delete()).then(s["a"])}function v(t,e){return fetch("".concat(r["d"].apiUrl,"/users/").concat(t,"/tasks?").concat(Object(r["c"])(e)),c["a"].get()).then(s["a"])}function g(t,e){return fetch("".concat(r["d"].apiUrl,"/users/").concat(t,"/leads?").concat(Object(r["c"])(e)),c["a"].get()).then(s["a"])}function b(t,e){return fetch("".concat(r["d"].apiUrl,"/users/").concat(t,"/accounts?").concat(Object(r["c"])(e)),c["a"].get()).then(s["a"])}function m(t,e){return fetch("".concat(r["d"].apiUrl,"/users/").concat(t,"/contacts?").concat(Object(r["c"])(e)),c["a"].get()).then(s["a"])}function y(t,e){return fetch("".concat(r["d"].apiUrl,"/users/").concat(e,"/change_avatar"),Object(n["a"])(Object(n["a"])({method:"POST"},_()),{},{body:t})).then(s["a"])}function _(){var t=o["a"].currentUserValue||{},e=t.jwt?{Authorization:t.jwt}:{};return{headers:Object(n["a"])({},e)}}},"99af":function(t,e,a){"use strict";var n=a("23e7"),r=a("d039"),c=a("e8b5"),s=a("861d"),o=a("7b0b"),i=a("50c4"),l=a("8418"),u=a("65f0"),d=a("1dde"),f=a("b622"),h=a("2d00"),p=f("isConcatSpreadable"),v=9007199254740991,g="Maximum allowed index exceeded",b=h>=51||!r((function(){var t=[];return t[p]=!1,t.concat()[0]!==t})),m=d("concat"),y=function(t){if(!s(t))return!1;var e=t[p];return void 0!==e?!!e:c(t)},_=!b||!m;n({target:"Array",proto:!0,forced:_},{concat:function(t){var e,a,n,r,c,s=o(this),d=u(s,0),f=0;for(e=-1,n=arguments.length;e<n;e++)if(c=-1===e?s:arguments[e],y(c)){if(r=i(c.length),f+r>v)throw TypeError(g);for(a=0;a<r;a++,f++)a in c&&l(d,f,c[a])}else{if(f>=v)throw TypeError(g);l(d,f++,c)}return d.length=f,d}})},afa6:function(t,e,a){"use strict";a("1031")},defb:function(t,e,a){"use strict";a.r(e);var n=function(){var t=this,e=t.$createElement,a=t._self._c||e;return a("v-app",{attrs:{id:"dayspan"}},[a("VLoading",{attrs:{loading:t.loading}}),a("ds-calendar-app",{ref:"app",attrs:{calendar:t.calendar,"read-only":!0},scopedSlots:t._u([{key:"eventPopover",fn:function(e){return[a("ds-calendar-event-popover",t._b({attrs:{"read-only":t.readOnly}},"ds-calendar-event-popover",e,!1))]}},{key:"eventTimeTitle",fn:function(e){var n=e.calendarEvent,r=e.details;return[a("div",[r.icon?a("v-icon",{staticClass:"ds-ev-icon",style:{color:r.forecolor},attrs:{size:"14"}},[t._v(" "+t._s(r.icon)+" ")]):t._e(),a("strong",{staticClass:"ds-ev-title"},[t._v(t._s(r.title))])],1),a("div",{staticClass:"ds-ev-description"},[t._v(t._s(t.getCalendarTime(n)))])]}}])},[a("template",{slot:"title"},[t._v(" DaySpan ")]),a("template",{slot:"menuRight"},[a("v-btn",{attrs:{icon:"",large:"",href:"https://github.com/ClickerMonkey/dayspan-vuetify",target:"_blank"}},[a("v-avatar",{attrs:{size:"32px",tile:""}},[a("img",{attrs:{src:"https://simpleicons.org/icons/github.svg",alt:"Github"}})])],1)],1)],2)],1)},r=[],c=(a("d3b7"),a("159b"),a("9abd")),s=a("2b0e"),o=a("e118"),i=a("7082"),l=a("55b4"),u={name:"Calendar",components:{VLoading:o["a"]},data:function(){return{storeKey:"dayspanState",calendar:c["Calendar"].months(),readOnly:!0,loading:!0,defaultEvents:[],currentUser:null,tasks:[]}},created:function(){var t=localStorage.getItem("currentUser");this.currentUser=t?JSON.parse(t):null,this.loadTasks()},mounted:function(){window.app=this.$refs.app},methods:{loadTasks:function(){var t=this;this.tasks=[],this.loading=!0;var e={currentPage:1,pageSize:100};i["a"].getAllTasks(this.currentUser.id,e).then((function(e){e&&e.data&&(t.tasks=e.data.tasks,t.tasks.forEach((function(e){if(e.rrule){var a=l["a"].fromString(e.rrule);if(a){var n=a.all();n&&n.length>0&&n.forEach((function(a){a=new Date(a),t.defaultEvents.push({data:{title:e.title,color:"#3F51B5"},schedule:{on:c["Day"].fromDate(a),times:[new c["Time"](a.getHours(),a.getMinutes(),a.getSeconds())]}})}))}}})),t.loadState())})).finally((function(){t.loading=!1}))},getCalendarTime:function(t){var e=t.start.format("a"),a=t.end.format("a"),n=t.start.format("h"),r=t.end.format("h");return 0!==t.start.minute&&(n+=t.start.format(":mm")),0!==t.end.minute&&(r+=t.end.format(":mm")),e===a?n+" - "+r+a:n+e+" - "+r+a},loadState:function(){var t=this,e={};e.events&&e.events.length||(e.events=this.defaultEvents),e.events.forEach((function(e){var a=t.$dayspan.getDefaultEventDetails();e.data=s["default"].util.extend(a,e.data)})),this.$refs.app.setState(e)}}},d=u,f=(a("1206"),a("2877")),h=Object(f["a"])(d,n,r,!1,null,null,null);e["default"]=h.exports},e118:function(t,e,a){"use strict";var n=function(){var t=this,e=t.$createElement,a=t._self._c||e;return a("div",[t.loading?a("div",{staticClass:"loading"},[t._m(0)]):t._e()])},r=[function(){var t=this,e=t.$createElement,a=t._self._c||e;return a("div",{staticClass:"lds-facebook"},[a("div"),a("div"),a("div")])}],c={name:"VLoading",props:{loading:Boolean}},s=c,o=(a("afa6"),a("2877")),i=Object(o["a"])(s,n,r,!1,null,"52be6f57",null);e["a"]=i.exports}}]);
//# sourceMappingURL=chunk-56b4b5bc.5167eb7f.js.map