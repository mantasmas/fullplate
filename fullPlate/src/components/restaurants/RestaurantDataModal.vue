<template>
  <md-dialog md-open-from="#btn-new-restaurant" md-close-to="#btn-new-restaurant" ref="new-restaurant-dialog">
    <md-dialog-title>Create New Restaurant</md-dialog-title>
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
        <md-input-container>
          <label>Restaurant Telephone number</label>
          <md-input
              v-model="editedRestaurant.telephoneNumber"
              md-clearable="true"
          />
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
  import {mapGetters} from 'vuex';

  export default {
    name: 'restaurant-data-modal',
    props: ['editMode', 'restaurant'],
    data () {
      return {};
    },
    methods: {
      closeDialog () {
        console.log(this.editedRestaurant.name);
        console.log(!this.editedRestaurant.name);
        this.$refs['new-restaurant-dialog'].close();
      },
      submitData () {
        this.$refs['new-restaurant-dialog'].close();

        if (!this.editMode) {
          this.$store.dispatch('addNewRestaurant', this.editedRestaurant);
        } else {
          this.$store.dispatch('updateRestaurant', this.editedRestaurant);
        }
      }
    },
    computed: {
      ...mapGetters(['editedRestaurant'])
    }
  };
</script>