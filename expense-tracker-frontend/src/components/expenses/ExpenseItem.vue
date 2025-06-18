<template>
  <li
    class="relative border border-border dark:border-border-dark p-3 pr-24 rounded bg-background-soft dark:bg-background-soft-dark"
  >
    <div class="text-sm sm:text-base space-y-1 sm:space-y-0 sm:space-x-2 sm:flex sm:flex-wrap">
      <span class="block sm:inline"
        ><em>{{ expense.category }}</em></span
      >
      <span class="block sm:inline font-semibold text-heading dark:text-heading-dark">{{
        expense.title
      }}</span>
      <span class="block sm:inline">{{ expense.amount }} RON</span>
      <span class="block sm:inline">Date: {{ expense.date }}</span>
      <span v-if="expense.notes" class="block sm:inline">Note: {{ expense.notes }}</span>
    </div>

    <div class="absolute top-2 right-2 flex flex-col sm:flex-row gap-1 sm:gap-1">
      <button
        @click="$emit('edit', expense)"
        class="bg-logo-blue text-white px-3 py-1 rounded hover:bg-logo-blue-dark transition focus:outline-none focus:ring-2 focus:ring-logo-blue"
      >
        Edit
      </button>
      <button
        @click="$emit('delete', expense)"
        class="bg-logo-blue text-white px-3 py-1 rounded hover:bg-logo-blue-dark transition focus:outline-none focus:ring-2 focus:ring-logo-blue"
      >
        Delete
      </button>
    </div>
  </li>
</template>

<script setup>
defineProps({
  expense: {
    type: Object,
    required: true,
  },
})

defineEmits(['edit', 'delete'])
</script>
