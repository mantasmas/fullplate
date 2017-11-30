<template>
  <md-dialog md-open-from="#btn-new-restaurant" md-close-to="#btn-new-restaurant" ref="new-restaurant-dialog">
    <md-dialog-title v-if="editMode">Edit Restaurant</md-dialog-title>
    <md-dialog-title v-else>Create New Restaurant</md-dialog-title>
    <form v-on:submit.prevent="submitData">
      <md-dialog-content>
        <md-input-container>
          <label>Restaurant Name</label>
          <md-input
              v-model="editedRestaurant.name"
              md-clearable="true"
              :required="true"
          />
        </md-input-container>
        <md-input-container>
          <label>Restaurant Address</label>
          <md-input
              v-model="editedRestaurant.address"
              md-clearable="true"
          />
        </md-input-container>
        <md-input-container :class="{'md-input-invalid': errors.has('numeric')}">
          <label>Restaurant Telephone number</label>
          <md-input
              v-model="editedRestaurant.telephoneNumber"
              md-clearable="true"
              v-validate
              data-vv-name="numeric"
              data-vv-rules="numeric"
          />
          <span class="md-error">{{errors.first('numeric')}}</span>
        </md-input-container>
      </md-dialog-content>

      <md-dialog-actions>
        <md-button @click="closeDialog">Cancel</md-button>
        <md-button
            type="submit">Add
        </md-button>
      </md-dialog-actions>
    </form>
  </md-dialog>
</template>

<script>
  export default {
    name: 'restaurant-data-modal',
    props: ['editMode', 'restaurantData'],
    data () {
      return {
        editedRestaurant: {
          id: 0,
          name: '',
          address: '',
          telephoneNumber: ''
        }
      };
    },
    methods: {
      closeDialog () {
        this.$refs['new-restaurant-dialog'].close();
      },
      submitData () {
        if (!this.errors.items.length) {
          this.$refs['new-restaurant-dialog'].close();

          if (!this.editMode) {
            this.$store.dispatch('addNewRestaurant', this.editedRestaurant);
          } else {
            this.$store.dispatch('updateRestaurant', this.editedRestaurant);
          }
        }
      }
    },
    watch: {
      restaurantData: function () {
        this.editedRestaurant = this.restaurantData;
      }
    }
  };
</script>