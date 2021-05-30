<template>
  <v-app id="dayspan" v-cloak>
    <VLoading :loading="loading"/>
    <ds-calendar-app ref="app"
                     :calendar="calendar"
                     :read-only="true">

      <template slot="title">
        DaySpan
      </template>

      <template slot="menuRight">
        <v-btn icon large href="https://github.com/ClickerMonkey/dayspan-vuetify" target="_blank">
          <v-avatar size="32px" tile>
            <img src="https://simpleicons.org/icons/github.svg" alt="Github">
          </v-avatar>
        </v-btn>
      </template>

      <template slot="eventPopover" slot-scope="slotData">
        <ds-calendar-event-popover
            v-bind="slotData"
            :read-only="readOnly"
        ></ds-calendar-event-popover>
      </template>


      <template slot="eventTimeTitle" slot-scope="{calendarEvent, details}">
        <div>
          <v-icon class="ds-ev-icon"
                  v-if="details.icon"
                  size="14"
                  :style="{color: details.forecolor}">
            {{ details.icon }}
          </v-icon>
          <strong class="ds-ev-title">{{ details.title }}</strong>
        </div>
        <div class="ds-ev-description">{{ getCalendarTime( calendarEvent ) }}</div>
      </template>

    </ds-calendar-app>

  </v-app>
</template>

<script>
import { Calendar, Day, Time } from 'dayspan';
import Vue from 'vue';
import VLoading from "@/components/common/VLoading";
import {userService} from "@/service/user.service";
import {RRule} from 'rrule';


export default {

  name: 'Calendar',
  components: {VLoading},
  data: () => ({
    storeKey: 'dayspanState',
    calendar: Calendar.months(),
    readOnly: true,
    loading: true,
    defaultEvents: [
    ],
    currentUser: null,
    tasks: []
  }),
  created() {
    let currentUser = localStorage.getItem('currentUser');
    this.currentUser = currentUser ? JSON.parse(currentUser) : null;
    this.loadTasks();
  },

  mounted()
  {
    window.app = this.$refs.app;

  },

  methods:
      {
        loadTasks() {
          this.tasks = [];
          this.loading = true;
          let query = {
            currentPage: 1,
            pageSize: 100
          };
          userService.getAllTasks(this.currentUser.id, query).then(res => {
            if (res && res.data) {
              this.tasks = res.data.tasks;
              this.tasks.forEach(task => {
                if (task.rrule) {
                  const rrule = RRule.fromString(task.rrule);
                  if (rrule) {
                    const events = rrule.all();
                    if (events && events.length > 0) {
                      events.forEach(event => {
                        event = new Date(event);
                        this.defaultEvents.push({
                          data: {
                            title: task.title,
                            color: '#3F51B5'
                          },
                          schedule: {
                            on: Day.fromDate(event),
                            times: [new Time(event.getHours(), event.getMinutes(), event.getSeconds())]
                          }
                        });
                      })
                    }
                  }
                }
              })
              this.loadState();
            }
          }).finally(() => {
            this.loading = false;
          })
        },
        getCalendarTime(calendarEvent)
        {
          let sa = calendarEvent.start.format('a');
          let ea = calendarEvent.end.format('a');
          let sh = calendarEvent.start.format('h');
          let eh = calendarEvent.end.format('h');

          if (calendarEvent.start.minute !== 0)
          {
            sh += calendarEvent.start.format(':mm');
          }

          if (calendarEvent.end.minute !== 0)
          {
            eh += calendarEvent.end.format(':mm');
          }

          return (sa === ea) ? (sh + ' - ' + eh + ea) : (sh + sa + ' - ' + eh + ea);
        },
        loadState()
        {
          let state = {};
          if (!state.events || !state.events.length)
          {
            state.events = this.defaultEvents;
          }
          state.events.forEach(ev =>
          {
            let defaults = this.$dayspan.getDefaultEventDetails();
            ev.data = Vue.util.extend( defaults, ev.data );
          });
          this.$refs.app.setState( state );
        }
      }
}
</script>

<style>
@import '~vuetify/dist/vuetify.css';
@import '~material-design-icons-iconfont/dist/material-design-icons.css';
@import '~dayspan-vuetify/dist/lib/dayspan-vuetify.min.css';
body, html, #app, #dayspan {
  font-family: Roboto, sans-serif !important;
  width: 100%;
  height: 100%;
}

.v-btn--flat,
.v-text-field--solo .v-input__slot {
  background-color: #f5f5f5 !important;
  margin-bottom: 8px !important;
}

</style>
