<template>
  <div class="">

    <div class="px-5 pt-3 m-0 mb-3 background-main" style="position: relative">
      <VLoading v-if="isLoading"/>
      
      <div class="row ">
        <div class="col-sm-9">
        </div>
        <div class="col-sm-3 d-flex justify-content-end">
          <router-link to="/group-list">
            <VButton :data="btnCancel"/>
          </router-link>
          <span class="ml-5" @click="save()"><VButton :data="btnSave"/></span>
        </div>
      </div>
      <div class=" mt-2">

        <div class="row mt-3">
          <div class="basic-edit">
            <h4 class="ml-2"><strong>Group Information</strong></h4>
            <div class="row ml-2">
              <div class="col-sm-5">
                <table>
                  <tbody>
                  <tr>
                    <td class="required">Group Name</td>
                    <td>
                      <input :class="{'is-invalid': !groupName && isCheck}" class="form-control" type="text"
                             v-model="groupName">
                      <div class="invalid-feedback" v-show="!groupName && isCheck">
                        {{ 'Chưa nhập thông tin!' }}
                      </div>
                    </td>
                  </tr>
                  </tbody>
                </table>
              </div>
            </div>
          </div>
        </div>
        <div class="row mt-3">
          <TableInDetail :header-columns="columns" :title="'Permission'">
            <template slot="body">
              <tbody v-if="permissions">
              <template v-for="(item) in permissions">
                <tr :key="item.permissionGroupId">
                  <td scope="row">{{ item.permissionGroupName }}</td>
                  <td scope="row"></td>
                  <td scope="row"></td>
                  <td>
                    <input type="checkbox" @change="onCheckGroup(item, $event)" >
                  </td>
                </tr>
                <tr v-for="(p, idx) in item.permissions" :key="'p'+item.permissionGroupId + idx">
                  <td scope="row"></td>
                  <td scope="row">{{ p.name }}</td>
                  <td scope="row">{{ p.description }}</td>
                  <td>
                    <input type="checkbox" :value="p.id" v-model="permissionSelected">
                  </td>
                </tr>
              </template>
              </tbody>
            </template>
          </TableInDetail>
        </div>
        <div class="row">
          <div class="col-sm-12 basic-edit">
            <h4 class="ml-2"><strong>Permissions</strong></h4>
            <div class="row" style="margin-top: 20px;">
              <div class="col-md-1"></div>
              <div class="col-md-10">
                <div v-for="item in permissions" :key="item.permissionGroupId">
                  <collapsible :isOpen="true">
                    <h5 slot="trigger" style="color: #D93915;">{{item.permissionGroupName}} <i-arrow-down size="15"/></h5>
                    <h5 slot="closedTrigger" style="color: #D93915;">{{item.permissionGroupName}} <i-arrow-up size="15"/></h5>
                    <table>
                      <thead>
                        <th>Permission Name</th>
                        <th>Description</th>
                        <th>Selected</th>
                      </thead>
                      <tbody>
                        <tr v-for="{id, name, description} in item.permissions" :key="id">
                          <td>{{name}}</td>
                          <td>{{description}}</td>
                          <td><input type="checkbox" :value="id" v-model="permissionSelected"></td>
                        </tr>

                      </tbody>
                    </table>
                  </collapsible>
                  <div class="dropdown-divider" style="margin-bottom: 1.5rem"></div>
                </div>
              </div>
              <div class="col-md-1"></div>
            </div>
            
            
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import TableInDetail from "@/components/common/table/TableInDetail";

import VButton from "@/components/common/VButton";
import {groupService} from "@/service/group.service";
import VLoading from "@/components/common/VLoading";
import 'vue-collapsible-component/lib/vue-collapsible.css';
import Collapsible from 'vue-collapsible-component';
import ArrowDown from 'vue-material-design-icons/ArrowDown.vue';
import ArrowUp from 'vue-material-design-icons/ArrowUp.vue';

export default {
  name: "GroupUpdate",
  components: {
    VLoading, 
    VButton, 
    TableInDetail,
    "collapsible": Collapsible,
    "i-arrow-down": ArrowDown,
    "i-arrow-up": ArrowUp,
  },
  methods: {
    save() {
      this.isCheck = true;
      if (!this.groupName || !this.groupName.trim()) {
        return;
      }
      this.isLoading = true;
      this.isCheck = false;
      if (!this.groupId) {
        groupService.create({
          name: this.groupName,
          permissions: this.permissionSelected
        }).then(res => {
          alert(res.message);
          this.$router.push('/group-list');
        }).catch(err => alert(err));
      } else {
        groupService.update({
          name: this.groupName,
          permissions: this.permissionSelected
        }, this.groupId).then((res) => {
          alert(res.message);
          this.$router.push('/group-list');
        }).catch(err => alert(err));
      }
    },
    loadPermission() {
      this.isLoading = true;
      this.permissions = [];
      groupService.getAllPermission()
          .then(res => {
            console.log(res.data)
            if (res && res.data) {
              this.permissions = res.data.perms;
            }
            this.isLoading = false;
          }).catch(err => alert(err));
    },
    onCheckGroup(permissionGroup, event) {
      if (permissionGroup.permissions) {
        permissionGroup.permissions.forEach(p => {
          const idx = this.permissionSelected.indexOf(p.id);
          if (event.target.checked) {
            if (idx < 0) {
              this.permissionSelected.push(p.id);
            }
          } else {
            if (idx > -1) {
              this.permissionSelected.splice(idx, 1);
            }
          }
        });
      }
    },
    loadGroup() {
      this.isLoading = true;
      this.permissionSelected = [];
      groupService.getById(this.groupId).then(res => {
        if (res && res.data) {
          this.groupName = res.data.name;
          res.data.perms.forEach(group => {
            group.permissions.forEach(p => {
              if (p.isChecked) {
                this.permissionSelected.push(p.id);
              }
            });
          })
          this.isLoading = false;

        }
      }).catch(err => alert(err));
    }
  },
  created() {
    if (this.$route.query.id) {
      this.groupId = this.$route.query.id;
      this.loadGroup();
    }
    this.loadPermission();
  },
  data() {
    return {
      columns: ['Permission Grouping', 'Permission Name', 'Description', 'Assign'
      ],
      groupName: null,
      groupId: null,
      isCheck: false,
      permissions: [],
      permissionSelected: [],
      btnCancel: {btnClass: 'btn-white px-3', icon: 'fa-times', text: 'Cancel'},
      btnSave: {btnClass: 'btn-red px-4', icon: 'fa-floppy-o', text: 'Save'},
      isLoading: false
    }
  }
}
</script>

<style scoped>
  button:focus {
    outline: none !important;
  }

  table thead th {
   color: #9FA2B4;
   border-bottom: 1px solid rgba(0,0,0, 0.1);
   text-align: center;
 }

  table tbody tr {
   text-align: center;
 }

</style>