# Epic F — Workflow & Notifications (P1)
### F1. Request → Approve Change
**Story:** As a System Owner, I can request a sensitive change that an Architect approves.
**AC:**
- Given a locked attribute
  When I submit a change request
  Then an approver gets notified and can Approve/Reject
**DoD:** Policies, SignalR banner, email stub.
**Size:** L

### F2. Reminders
**Story:** As PO, I want reminders for stale ownership and pending updates.
**AC:**
- Given inactivity threshold
  When a system hasn't been updated for 90 days
  Then owners receive reminders weekly
**DoD:** Scheduled job, throttling, audit.
**Size:** M

---