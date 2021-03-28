<template>
  <div class="w-100">
    <div class="time-line row m-0 p-3">
      <div class="ml-3 w-100">
        <h4><strong>Timeline</strong></h4>
        <div class="time-line-status">
          <ul v-if="timelineStatus" class="timeline timeline-horizontal w-100">
            <li v-for="(timeline, index) in timelineStatus" :key="index" class="timeline-item">
              <div class="timeline-badge " :class="[timeline.status === 1 ? 'primary-bg' : 'primary']"></div>
              <div class="timeline-text">
                {{timeline.text}}
              </div>
            </li>
          </ul>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "VTimeLine",
  data: function () {
    return {
      timelineStatus: [
        {id: 1, text: 'Qualified', status: 1},
        {id: 2, text: 'Value Proposition', status: 1},
        {id: 3, text: 'Find key contacts', status: 1},
        {id: 4, text: 'Send Proposal', status: 1},
        {id: 5, text: 'Review', status: 0},
        {id: 6, text: 'Negotiate', status: 0},
        {id: 7, text: 'Won', status: 0},
        {id: 8, text: 'Lost', status: 0}
      ]
    }
  }
}
</script>

<style scoped>
.time-line {
  background: #FFFFFF;
  box-shadow: 0px -8px 10px rgba(255, 255, 255, 0.5), 0px 16px 24px rgba(55, 71, 79, 0.2);
  border-radius: 30px;
}

.time-line-status {
  display: inline-block;
  width: 100%;
  overflow-y: auto;
}
.timeline-horizontal {
  list-style: none;
  position: relative;
  display: inline-block;
}
.timeline:before {
  bottom: 0;
  position: absolute;
  content: " ";
  width: 3px;
  background-color: #D93915;
}

.timeline-horizontal:before {
  height: 3px;
  top: 33%;
  right: 0;
  width: 100%;
  margin-bottom: 20px;
}

.timeline .timeline-item {
  margin-bottom: 20px;
  position: relative;
}

.timeline-horizontal .timeline-item {
  display: table-cell;
  height: 80px;
  min-width: 175px;
  float: none !important;
  padding-left: 0px;
  vertical-align: bottom;
}

.timeline .timeline-item .timeline-badge.primary-bg {
  background-color: #D93915;
}

.timeline .timeline-item .timeline-badge.primary {
  background-color: white;
  border: 3px solid #D93915;
}

.timeline-horizontal .timeline-item .timeline-badge {
  top: auto;
  bottom: 0px;
  left: 43px;
}

.timeline .timeline-item .timeline-badge {
  color: #fff;
  width: 30px;
  height: 30px;
  line-height: 30px;
  font-size: 22px;
  text-align: center;
  position: absolute;
  top: 12px;
  left: 20%;
  background-color: #333;
  z-index: 100;
  border-top-right-radius: 50%;
  border-top-left-radius: 50%;
  border-bottom-right-radius: 50%;
  border-bottom-left-radius: 50%;
}
.timeline-text {
  position: absolute;
  top: 55px;
  left: -20%;
  width: 100%;
  text-align: center;
}
</style>