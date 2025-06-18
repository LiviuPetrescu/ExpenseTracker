<template>
  <div class="flex flex-col sm:flex-row sm:space-x-3 space-y-2 sm:space-y-0 pt-1">
    <!-- Submit Button -->
    <button
      type="submit"
      class="bg-logo-blue text-white px-4 py-2 rounded hover:bg-logo-blue-dark transition focus:outline-none focus:ring-2 focus:ring-logo-blue"
    >
      {{ isEditMode ? 'Update Expense' : 'Add Expense' }}
    </button>

    <!-- Cancel Button -->
    <button
      v-if="isEditMode"
      type="button"
      @click="$emit('cancel')"
      class="bg-gray-200 text-gray-800 px-4 py-2 rounded hover:bg-gray-300 transition focus:outline-none focus:ring-2 focus:ring-gray-400"
    >
      Cancel
    </button>
  </div>
</template>

<script setup>
defineProps({
  isEditMode: Boolean,
})
defineEmits(['cancel'])
</script>
