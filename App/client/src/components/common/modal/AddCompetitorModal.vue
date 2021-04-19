<template>
  <transition name="modal-fade">
    <div class="modal-backdrop" role="dialog">
      <div class="modal" ref="modal" style="position: relative">
        <VLoading :loading="loading"/>
        <header class="modal-header">
          <slot name="header">
            <h4 class="text-center w-100">Add Competitor</h4>
          </slot>
        </header>
        <section class="modal-body text-center">
          <slot name="body">
            <table class="col-sm-12 table-in-detail">
              <tr class="row mt-2" :class="{ 'form-group--error': $v.competitor.name.$error }">
                <td class="col-sm-2 text-left ml-3">Name</td>
                <td class="col-sm-6"><input class="w-100 form-control" type="text" v-model.trim="competitor.name"></td>
              </tr>
              <tr class="row mt-2" :class="{ 'form-group--error': $v.competitor.website.$error }">
                <td class="col-sm-2 text-left ml-3">Website</td>
                <td class="col-sm-6"><input class="w-100 form-control" type="text" v-model.trim="competitor.website"></td>
              </tr>
              <tr class="row mt-2">
                <td class="col-sm-2 text-left ml-3">Threat</td>
<!--                <td class="col-sm-6"><input class="w-100 form-control" type="text" v-model.trim="competitor.threat"></td>-->
                <td class="col-sm-6">
                  <select class="form-control py-0 w-100" id="Priority" v-model.trim="competitor.threat">
                    <option :value="'LOW'">LOW</option>
                    <option :value="'MEDIUM'">MEDIUM</option>
                    <option :value="'HIGH'">HIGH</option>
                    <option :value="'SEND'">SEND</option>
                  </select>
                </td>
              </tr>
              <tr class="row mt-2">
                <td class="col-sm-2 text-left ml-3">Strengths</td>
                <td class="col-sm-8 text-left"><input v-model.trim="competitor.strengths" class="w-100 form-control" type="text" placeholder="Type something here..."></td>
              </tr>
              <tr class="row mt-2">
                <td class="col-sm-2 text-left ml-3">Weaknesses</td>
                <td class="col-sm-8 text-left"><input v-model.trim="competitor.weaknesses" class="w-100 form-control"  type="text" placeholder="Type something here..."></td>
              </tr>
              <tr class="row mt-2">
                <td class="col-sm-2 text-left ml-3">Suggestions</td>
                <td class="col-sm-8 text-left"><input v-model.trim="competitor.suggestions" class="w-100 form-control"  type="text" placeholder="Type something here..."></td>
              </tr>
            </table>
          </slot>
        </section>

        <footer class="modal-footer text-center">
          <slot name="footer">
            <span @click="close"><VButton :data="btnCancel"/></span>
            <span @click="save"><VButton :data="btnSend"/></span>
          </slot>
        </footer>
      </div>
    </div>
  </transition>
</template>

<script>
import {required} from "vuelidate/lib/validators";
import {dealService} from "@/service/deal.service";
import VLoading from "@/components/common/VLoading";
import VButton from "@/components/common/VButton";
export default {
  name: "AddCompetitorModal",
  props: {

  },
  validations: {
    competitor: {
      name: {
        required
      },
      website: {
        required
      }
    }
  },
  components: {VButton, VLoading},
  data () {
    return {
      competitor: {
        id: 0,
        name: null,
        website: null,
        threat: null,
        strengths: null,
        weaknesses: null,
        suggestions: null
      },
      loading: false,
      dealId: null,
      btnCancel: {btnClass: 'btn-white px-3 mr-4', icon: 'fa-times-circle', text: 'Cancel'},
      btnSend: {btnClass: 'btn-red px-4', icon: 'fa-floppy-o', text: 'Save'},
    }
  },
  methods: {
    refresh() {
      this.loading = false;
      this.competitor.name = null;
      this.competitor.website = null;
      this.competitor.threat = null;
      this.competitor.strengths = null;
      this.competitor.weaknesses = null;
      this.competitor.suggestions = null;
    },
    save() {
      this.$v.$touch()
      if (this.$v.$invalid) {
        alert('loi')
        return;
      }
      this.loading = true;
      dealService.createCompetitor(this.competitor, this.dealId)
      .then(res => {
        if (res && res.status === 'success') {
          alert(res.message);
          this.$emit('create-success');
        }
      })
    },
    close() {
      this.loading = false;
      this.$emit('close');
    },
  }
}
</script>

<style scoped>
  input:focus{
    outline: none;
  }
  .modal {
    width: 50%;
  }
  .modal-header h4 {
    color: black;
  }
</style>